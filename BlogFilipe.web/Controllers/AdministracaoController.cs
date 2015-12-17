using BlogFilipe.DB;
using BlogFilipe.DB.Classes;
using BlogFilipe.web.Models.Administracao;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFilipe.web.Controllers
{
    [Authorize]
    public class AdministracaoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region CadastrarPost
        [HttpGet]
        public ActionResult CadastrarPost()
        {
            var viewModel = new CadastrarPostViewModel();

            viewModel.Autor = "Filipe";
            viewModel.DataPublicacao = DateTime.Now;
            viewModel.HoraPublicacao = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var post = new Post();

                post.Titulo = viewModel.Titulo;
                post.Autor = viewModel.Autor;
                post.Resumo = viewModel.Resumo;
                post.Descricao = viewModel.Descricao;
                post.Visivel = viewModel.Visivel;
                post.DataPublicacao = viewModel.DataPublicacao.Add(viewModel.HoraPublicacao.TimeOfDay);
                post.TagsPost = new List<TagPost>();

                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.TagClass
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();

                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.TagClass.Add(tagClass);
                        }

                        var tagPost = new TagPost();
                        tagPost.IdTag = item;
                        post.TagsPost.Add(tagPost);
                    }
                }

                conexao.Posts.Add(post);

                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(viewModel);
        }
        #endregion

        #region EditarPost
        public ActionResult EditarPost(int id)
        {
            var viewModel = new CadastrarPostViewModel();
            var conexao = new ConexaoBanco();

            var post = (from x in conexao.Posts where x.Id == id select x).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(string.Format("Erro"));
            }
            else
            {
                viewModel.Autor = post.Autor;
                viewModel.DataPublicacao = post.DataPublicacao;
                viewModel.Descricao = post.Descricao;
                viewModel.HoraPublicacao = post.DataPublicacao;
                viewModel.Resumo = post.Resumo;
                viewModel.Titulo = post.Titulo;
                viewModel.Visivel = post.Visivel;
                viewModel.Tags = (from p in post.TagsPost
                                  select p.IdTag).ToList();
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var post = conexao.Posts.FirstOrDefault(x => x.Id == viewModel.Id);

                post.Titulo = viewModel.Titulo;
                post.Autor = viewModel.Autor;
                post.Resumo = viewModel.Resumo;
                post.Descricao = viewModel.Descricao;
                post.Visivel = viewModel.Visivel;
                post.DataPublicacao = viewModel.DataPublicacao.Add(viewModel.HoraPublicacao.TimeOfDay);

                var tagPostAtual = post.TagsPost.ToList();

                foreach (var item in tagPostAtual)
                {
                    conexao.TagPosts.Remove(item);
                }

                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.TagClass
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();

                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.TagClass.Add(tagClass);
                        }

                        var tagPost = new TagPost();
                        tagPost.IdTag = item;
                        post.TagsPost.Add(tagPost);
                    }
                }

                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(viewModel);
        }
        #endregion

        public ActionResult ExcluirPost(int id)
        {
            var conexao = new ConexaoBanco();

            var post = conexao.Posts.FirstOrDefault(x => x.Id == id);

            if( post == null)
            {
                throw new Exception(string.Format("Erro"));
            }

            conexao.Posts.Remove(post);
            conexao.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }

        #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                              where p.Id == id
                              select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Posts
                        where p.Id == comentario.IdPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.DataPublicacao.Year,
                mes = post.DataPublicacao.Month,
                dia = post.DataPublicacao.Day,
                titulo = post.Titulo,
                id = post.Id
            }) + "#comentarios");
        }
        #endregion
    }
}