using BlogFilipe.DB;
using BlogFilipe.DB.Classes;
using BlogFilipe.web.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFilipe.web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            var conexao = new ConexaoBanco();
            var viewModel = new ListarUsuarioViewModel();

            List<Usuario> usuarios = (from p in conexao.Usuarios
                                      select p).ToList();

            viewModel.Usuarios = usuarios.ToList();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var usuario = new Usuario();

                usuario.Login = viewModel.Login;
                usuario.Nome = viewModel.Nome;
                usuario.Senha = viewModel.Senha;

                var usuarioMesmoNome = (from p in conexao.Usuarios
                                        where p.Login.ToUpper() == usuario.Login.ToUpper()
                                        select p).Any();

                if (usuarioMesmoNome)
                {
                    ModelState.AddModelError("Login", "Usuário já cadastrado.");
                }
                else
                {
                    conexao.Usuarios.Add(usuario);

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

            }
            return View(viewModel);
        }

        #region EditarUsuario
        public ActionResult EditarUsuario(int id)
        {
            var viewModel = new CadastrarUsuarioViewModel();
            var conexao = new ConexaoBanco();

            var usuario = conexao.Usuarios.FirstOrDefault(x => x.Id == id);

            if (usuario == null)
            {
                throw new Exception(string.Format("Erro"));
            }
            else
            {
                viewModel.Login = usuario.Login;
                viewModel.Nome = usuario.Nome;
                viewModel.Senha = usuario.Senha;
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var usuario = conexao.Usuarios.FirstOrDefault(x => x.Id == viewModel.Id);

                usuario.Login = viewModel.Login;
                usuario.Nome = viewModel.Nome;
                usuario.Senha = viewModel.Senha;

                var usuarioMesmoNome = ( from p in conexao.Usuarios
                                        where p.Login.ToUpper() == usuario.Login
                                           && p.Id != usuario.Id
                                        select p).Any();

                if (usuarioMesmoNome)
                {
                    ModelState.AddModelError("Login", "Usuário já cadastrado.");
                }
                else
                {
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
            }
            return View(viewModel);
        }
        #endregion

        public ActionResult ExcluirUsuario(int id)
        {
            var conexao = new ConexaoBanco();

            var usuario = conexao.Usuarios.FirstOrDefault(x => x.Id == id);

            if (usuario == null)
            {
                throw new Exception(string.Format("Erro"));
            }

            conexao.Usuarios.Remove(usuario);
            conexao.SaveChanges();

            return RedirectToAction("Index", "Usuario");
        }
    }
}