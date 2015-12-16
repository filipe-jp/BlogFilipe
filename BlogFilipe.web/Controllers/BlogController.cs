using BlogFilipe.DB;
using BlogFilipe.DB.Classes;
using BlogFilipe.web.Models.Administracao;
using BlogFilipe.web.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFilipe.web.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index(int? pagina, string tag, string pesquisa)
        {
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var conexao = new ConexaoBanco();
            var viewModel = new ListarPostsViewModel();
            var registrosPorPagina = 10;

            List<Post> posts = (from p in conexao.Posts
                                where p.Visivel == true
                                select p).ToList();

            if (!string.IsNullOrEmpty(tag))
            {
                posts = (from p in posts
                         where p.TagsPost.Any(x => x.IdTag.ToUpper() == tag.ToUpper())
                         select p).ToList();
            }

            if (!string.IsNullOrEmpty(pesquisa))
            {
                posts = (from p in posts
                         where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                            || p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                            || p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                         select p).ToList();
            }

            var totalRegistros = posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdRegistrosPular = (indiceDaPagina * registrosPorPagina);
            var qtdePaginas = (int)Math.Ceiling((decimal)totalRegistros / registrosPorPagina);

            viewModel.Posts = (from p in posts
                               orderby p.DataPublicacao descending
                               select p ).Skip(qtdRegistrosPular).Take(registrosPorPagina).ToList();
            viewModel.TotalPaginas = qtdePaginas;
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.Tag = tag;
            viewModel.Tags = (from p in conexao.TagClass
                              where conexao.TagPosts.Any(x => x.IdTag == p.Tag)
                              orderby p.Tag
                              select p.Tag).ToList();
            viewModel.Pesquisa = pesquisa;

            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }

        public ActionResult Post(int id)
        {
            var viewModel = new DetalhesPostViewModel();
            var conexao = new ConexaoBanco();

            var post = (from x in conexao.Posts where x.Id == id select x).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(string.Format("Erro"));
            }
            else
            {
                viewModel.Id = post.Id;
                viewModel.Autor = post.Autor;
                viewModel.DataPublicacao = post.DataPublicacao;
                viewModel.Descricao = post.Descricao;
                viewModel.HoraPublicacao = post.DataPublicacao;
                viewModel.Resumo = post.Resumo;
                viewModel.Titulo = post.Titulo;
                viewModel.Tags = (from p in post.TagsPost
                                  select p.IdTag).ToList();
            }

            return View(viewModel);
        }
    }
}