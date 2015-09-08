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
    public class QuestaoController : Controller
    {
        private int idUsuario = 0;
        private readonly IQuestaoAppService _questaoApp;
        private readonly IFuncionarioAppService _funcionarioApp;
        private readonly IQuestionarioAppService _questionarioApp;        

        public QuestaoController(IQuestaoAppService questaoApp, IFuncionarioAppService funcionarioApp, IQuestionarioAppService questionarioApp)
        {
            _questaoApp = questaoApp;
            _funcionarioApp = funcionarioApp;
            _questionarioApp = questionarioApp;            
        }

        // GET: Questao
        public ActionResult Index(int id)
        {
            Questionario questionario = _questionarioApp.GetById(id);
            IEnumerable<QuestaoViewModel> questoes = Mapper.Map<IEnumerable<Questao>, IEnumerable<QuestaoViewModel>>(_questaoApp.buscaPorQuestionarioId(id));
            ViewBag.questionario = "Questionário: " + questionario.QuestionarioId.ToString() + " - " + questionario.descricao;
            ViewBag.idQuestionario = questionario.QuestionarioId;

            return View(questoes);
        }

        // GET: Questao/Details/5
        public ActionResult Details(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return RedirectToAction("AcessoNegado", "Questionario");
            }

            QuestaoViewModel questao = Mapper.Map<Questao, QuestaoViewModel>(_questaoApp.GetById(id));            
            return View(questao);
        }

        // GET: Questao/Create
        public ActionResult Create(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return RedirectToAction("AcessoNegado", "Questionario");
            }

            Questionario questionario = _questionarioApp.GetById(id);
            ViewBag.questionario = "Questionário: " + questionario.QuestionarioId.ToString() + " - " + questionario.descricao;
            ViewBag.idQuestionario = questionario.QuestionarioId;
            TempData["idQuestionario"] = questionario.QuestionarioId;

            return View();
        }

        // POST: Questao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (TempData["idQuestionario"] != null)
            {
                var idQuestionario = TempData["idQuestionario"];
                _questaoApp.incluiQuestao(Convert.ToInt32(idQuestionario), collection["texto"], collection["txtAlternativa1"], collection["txtAlternativa2"], collection["txtAlternativa3"], collection["txtAlternativa4"], collection["txtAlternativa5"]);
                
                return RedirectToAction("Index", new { id = idQuestionario });

            }

            return View();
        }

        // GET: Questao/Edit/5
        public ActionResult Edit(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return RedirectToAction("AcessoNegado", "Questionario");
            }
            
            QuestaoViewModel questao = Mapper.Map<Questao, QuestaoViewModel>(_questaoApp.GetById(id));            

            ViewBag.questionario = "Questionário: " + questao.QuestionarioId.ToString() + " - " + questao.questionario.descricao;
            ViewBag.idQuestionario = questao.QuestionarioId;
            TempData["idQuestionario"] = questao.QuestionarioId;

            return View(questao);
        }

        // POST: Questao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (TempData["idQuestionario"] != null)
            {
                var idQuestionario = TempData["idQuestionario"];
                _questaoApp.alteraQuestao(id, collection["texto"], collection["txtAlternativa1"], collection["txtAlternativa2"], collection["txtAlternativa3"], collection["txtAlternativa4"], collection["txtAlternativa5"]);

                return RedirectToAction("Index", new { id = idQuestionario });
            }

            return View();
        }

        // GET: Questao/Delete/5
        public ActionResult Delete(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            Questao questao = _questaoApp.GetById(id);

            if (questao != null)
            {
                int questionarioId = questao.QuestionarioId;
                _questaoApp.Remove(questao);                

                return RedirectToAction("Index", new { id = questionarioId});
            }

            return View();
        }
        
    }
}
