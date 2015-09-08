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
    public class AvaliacaoController : Controller
    {
        private int idUsuario = 0;

        private readonly IAvaliacaoAppService _avaliacaoApp;
        private readonly IFuncionarioAppService _funcionarioApp;
        private readonly IAlternativaAppService _alternativaApp;
        private readonly IQuestionarioAppService _questionarioApp;


        public AvaliacaoController(IAvaliacaoAppService avaliacaoApp, IFuncionarioAppService funcionarioApp, IAlternativaAppService alternativaApp, IQuestionarioAppService questionarioApp)
        {
            _avaliacaoApp = avaliacaoApp;
            _funcionarioApp = funcionarioApp;
            _alternativaApp = alternativaApp;
            _questionarioApp = questionarioApp;
        }

        // GET: Avaliacao
        public ActionResult Index()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            IEnumerable<AvaliacaoViewModel> avaliacoes = Mapper.Map<IEnumerable<Avaliacao>, IEnumerable<AvaliacaoViewModel>>(_avaliacaoApp.buscaPendentesPorAvaliadorId(idUsuario));

            return View(avaliacoes);
        }

        //Get
        public ActionResult PreencheAvaliacao(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            AvaliacaoViewModel avaliacao = Mapper.Map<Avaliacao, AvaliacaoViewModel>(_avaliacaoApp.GetById(id));

            if (idUsuario != avaliacao.FuncionarioAvaliadorId)
            {
                return View("AcessoNegado");
            }

            TempData["idAvaliacao"] = id;

            return View(avaliacao);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreencheAvaliacao(FormCollection collection)
        {
            int idAvaliacao = Convert.ToInt32(TempData["idAvaliacao"]);
            AvaliacaoViewModel avaliacaoVM = Mapper.Map<Avaliacao, AvaliacaoViewModel>(_avaliacaoApp.GetById(idAvaliacao));

            if (collection.Count < 2)
            {
                ModelState.AddModelError("respostas", "Favor preencher as respostas da avaliação.");                
                return View(avaliacaoVM);
            }

            if ((collection.Count - 1) != avaliacaoVM.questionario.questoes.Count())
            {
                ModelState.AddModelError("respostas", "Há questão sem respostas. Favor preencher as respostas de todas as questões da avaliação.");
                return View(avaliacaoVM);
            }

            Avaliacao avaliacao = _avaliacaoApp.GetById(idAvaliacao);

            for (int i = 1; i <= collection.Count - 1; i++)
            {
                Alternativa alternativa = _alternativaApp.GetById(Convert.ToInt32(collection[i]));                
                avaliacao.respostas.Add(alternativa);
            }

            avaliacao.dataPreenchimento = DateTime.Today;
            _avaliacaoApp.SalvarRespostas(avaliacao);

            return RedirectToAction("Index", "Home");
        }

        //Get
        public ActionResult listaPendentes()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegadoAdm");
            }

            IEnumerable<AvaliacaoViewModel> avaliacoes = Mapper.Map<IEnumerable<Avaliacao>, IEnumerable<AvaliacaoViewModel>>(_avaliacaoApp.buscaAvaliacoesPendentes());

            return View(avaliacoes);
        }

        public ActionResult Edit(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegadoAdm");
            }

            AvaliacaoViewModel avaliacao = Mapper.Map<Avaliacao, AvaliacaoViewModel>(_avaliacaoApp.GetById(id));

            ViewBag.FuncionarioAvaliadoId = new SelectList(_funcionarioApp.GetAll(), "FuncionarioId", "nome", avaliacao.FuncionarioAvaliadoId);
            ViewBag.FuncionarioAvaliadorId = new SelectList(_funcionarioApp.GetAll(), "FuncionarioId", "nome", avaliacao.FuncionarioAvaliadorId);
            ViewBag.QuestionarioId = new SelectList(_questionarioApp.GetAll(), "QuestionarioId", "descricao", avaliacao.QuestionarioId);


            return View(avaliacao);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AvaliacaoViewModel avaliacao)
        {
            if (ModelState.IsValid)
            {
                Avaliacao ava = Mapper.Map<AvaliacaoViewModel, Avaliacao>(avaliacao);
                ava.AdministradorId = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
                _avaliacaoApp.Update(ava);
                return RedirectToAction("listaPendentes");
            }

            return View(avaliacao);
        }

    }
}