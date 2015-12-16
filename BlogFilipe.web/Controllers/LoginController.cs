using BlogFilipe.DB;
using BlogFilipe.web.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogFilipe.web.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel viewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var conexao = new ConexaoBanco();
            //var usuario = (from p in conexao.Usuarios
            //               where p.Login.ToUpper() == viewModel.Login.ToUpper()
            //                  && p.Senha == viewModel.Senha
            //               select p).FirstOrDefault();

            //if(usuario == null)
            //{
            //    ModelState.AddModelError("", "Usuário e/ou senha estão incorretos.");
            //    return View(viewModel);
            //}

            var usuario = (from p in conexao.Usuarios
                           where p.Login.ToUpper() == viewModel.Login.ToUpper()
                           select p).FirstOrDefault();

            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Usuário inválido.");
                return View(viewModel);
            }

            if (usuario.Senha != viewModel.Senha) {
                ModelState.AddModelError("Senha", "Senha incorreta.");
                return View(viewModel);
            }
            
            FormsAuthentication.SetAuthCookie(usuario.Login, viewModel.Lembrar);

            if(ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Blog");
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}