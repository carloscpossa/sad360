using SAD360.Application.Interfaces;
using SAD360.Domain.Entities;
using SAD360.MVC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAD360.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IFuncionarioAppService _funcionarioApp;

        public LoginController(IFuncionarioAppService funcionarioApp)
        {
            _funcionarioApp = funcionarioApp;
        }


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection)
        {

            string usuario = collection["txtUsuario"];
            string senha = collection["txtSenha"];

            Funcionario func = _funcionarioApp.buscaPorMatriculaSenha(usuario, senha);

            if (func == null)
            {
                ViewBag.UsuarioInvalido = func == null;
                return View();
            }
            
            if (collection["chbLembra"] != null)
            {                
                System.Web.Security.FormsAuthentication.SetAuthCookie(new Criptografia().Encrypt(func.FuncionarioId.ToString()), true);                
            }
            else
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(new Criptografia().Encrypt(func.FuncionarioId.ToString()), false);
            }            

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logoff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}