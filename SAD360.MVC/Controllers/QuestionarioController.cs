using AutoMapper;
using SAD360.Application.Interfaces;
using SAD360.Domain.Entities;
using SAD360.MVC.Security;
using SAD360.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAD360.MVC.Controllers
{
    [Authorize]
    public class QuestionarioController : Controller
    {
        private int idUsuario = 0;
        private readonly IQuestionarioAppService _questionarioApp;
        private readonly IFuncionarioAppService _funcionarioApp;

        public QuestionarioController(IQuestionarioAppService questionarioApp, IFuncionarioAppService funcionarioApp)
        {
            _questionarioApp = questionarioApp;
            _funcionarioApp = funcionarioApp;
        }

        // GET: Questionario
        public ActionResult Index()
        {
            IEnumerable<QuestionarioViewModel> questionarios = Mapper.Map<IEnumerable<Questionario>, IEnumerable<QuestionarioViewModel>>(_questionarioApp.GetAll());
            return View(questionarios);
        }

        // GET: Questionario/Details/5
        public ActionResult Details(int id)
        {
            QuestionarioViewModel questionario = Mapper.Map<Questionario, QuestionarioViewModel>(_questionarioApp.GetById(id));
            return View(questionario);
        }

        // GET: Questionario/Create
        public ActionResult Create()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }
            
            return View();
        }

        // POST: Questionario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionarioViewModel questionario)
        {
            if (ModelState.IsValid)
            {
                Questionario quest = Mapper.Map<QuestionarioViewModel, Questionario>(questionario);
                quest.AdministradorId = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
                _questionarioApp.Add(quest);
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

        // GET: Questionario/Edit/5
        public ActionResult Edit(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            QuestionarioViewModel questionario = Mapper.Map<Questionario, QuestionarioViewModel>(_questionarioApp.GetById(id));
            return View(questionario);
        }

        // POST: Questionario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionarioViewModel questionario)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            if (ModelState.IsValid)
            {
                Questionario quest = Mapper.Map<QuestionarioViewModel, Questionario>(questionario);
                quest.AdministradorId = this.idUsuario;
                _questionarioApp.Update(quest);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Questionario/Delete/5
        public ActionResult Delete(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            Questionario questionario = _questionarioApp.GetById(id);

            if (questionario != null)
            {
                _questionarioApp.Remove(questionario);
            }

            return RedirectToAction("Index");
        }

        //Get
        public ActionResult AcessoNegado()
        {
            return View();
        }
        
    }
}
