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
    public class RelatorioController : Controller
    {
        private int idUsuario = 0;

        private readonly IFuncionarioAppService _funcionarioApp;
        private readonly IAvaliacaoAppService _avaliacaoApp;
        private readonly IQuestionarioAppService _questionarioApp;

        public RelatorioController(IFuncionarioAppService funcionarioApp, IAvaliacaoAppService avaliacaoApp, IQuestionarioAppService questionarioApp)
        {
            _funcionarioApp = funcionarioApp;
            _avaliacaoApp = avaliacaoApp;
            _questionarioApp = questionarioApp;
        }


        // GET: Relatorio
        public ActionResult Index()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);

            if (func.ehGerente | func.ehAdministrador)
            {
                return RedirectToAction("RelatorioGerente");
            }

            ViewBag.funcionario = func.nome;            
            ViewBag.QuestionarioId = new SelectList(_questionarioApp.GetAll(), "QuestionarioId", "descricao");

            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            DateTime dataInicio = Convert.ToDateTime("01/01/1900");
            DateTime dataFim = Convert.ToDateTime("31/12/2999");
            int? questionarioId = null;

            
            try
            {
                dataInicio = Convert.ToDateTime(collection["dataInicio"]);
            }
            catch
            {
                dataInicio = DateTime.Parse("01/01/1900");
            }

            try
            {
                dataFim = Convert.ToDateTime(collection["dataFim"]);
            }
            catch
            {
                dataFim = DateTime.Parse("31/12/2999");
            }

            try
            {
                questionarioId = Convert.ToInt32(collection["QuestionarioId"]);
            }
            catch
            {
                questionarioId = null;
            }

            List<QuestionarioViewModel> lista = retornaQuestionarios(idUsuario, dataInicio, dataFim, questionarioId);
            TempData["questionarios"] = lista;


            return RedirectToAction("VisualizaRelatorio");
        }

        private List<QuestionarioViewModel> retornaQuestionarios(int idUsuario, DateTime dataInicio, DateTime dataFim, int? questionarioId)
        {
            if (dataInicio > Convert.ToDateTime("01/01/1900"))
            {
                TempData["filtroData"] = string.Format("{0:dd/MM/yyyy}", dataInicio) + " a " + string.Format("{0:dd/MM/yyyy}", dataFim);
            }
            else
            {
                TempData["filtroData"] = "Todos";
            }

            Funcionario func = _funcionarioApp.GetById(idUsuario);
            TempData["avaliado"] = func.nome;

            if (questionarioId != null)
            {
                TempData["questionario"] = _questionarioApp.GetById(Convert.ToInt32(questionarioId)).descricao;
            }
            else
            {
                TempData["questionario"] = "Todos";
            }



            IEnumerable<Avaliacao> avaliacoes = _avaliacaoApp.pesquisa(idUsuario, dataInicio, dataFim, questionarioId);

            List<Questionario> listaQuestionario = new List<Questionario>();
            foreach (Avaliacao item in avaliacoes)
            {
                try
                {
                    var quest = listaQuestionario.Where(q => q.QuestionarioId == item.QuestionarioId).Single();
                }
                catch
                {
                    listaQuestionario.Add(item.questionario);
                }
            }

            List<QuestionarioViewModel> questionarios = Mapper.Map<List<Questionario>, List<QuestionarioViewModel>>(listaQuestionario);

            List<Alternativa> respostas = new List<Alternativa>();

            foreach (Avaliacao item in avaliacoes)
            {
                foreach (Alternativa alt in item.respostas)
                {
                    respostas.Add(alt);
                }
            }

            var somaAlternativas = (from r in respostas
                                    group r by r.AlternativaId into g
                                    select new
                                    {
                                        alternativa = g.Key,
                                        questaoId = g.Min(r => r.QuestaoId),
                                        qtd = g.Count()
                                    }).ToList();

            foreach (QuestionarioViewModel item in questionarios)
            {
                foreach (QuestaoViewModel questao in item.questoes)
                {
                    double total = somaAlternativas.Where(r => r.questaoId == questao.QuestaoId).Sum(r => r.qtd);
                    foreach (AlternativaViewModel alt in questao.alternativas)
                    {
                        double qtd = somaAlternativas.Where(r => r.alternativa == alt.AlternativaId).Sum(r => r.qtd);
                        alt.percentual = (qtd * 100) / total;
                    }
                }
            }

            return questionarios;
        }

        //Get
        public ActionResult VisualizaRelatorio()
        {
            if (TempData["questionarios"] == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.avaliado = TempData["avaliado"];
            ViewBag.filtroData = TempData["filtroData"];
            ViewBag.questionario = TempData["questionario"];                           
            
            List<QuestionarioViewModel> questionatios = TempData["questionarios"] as List<QuestionarioViewModel>;

            return View(questionatios);
        }

        //Get
        public ActionResult RelatorioGerente()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);

            if (!func.ehGerente && !func.ehAdministrador)
            {
                return RedirectToAction("Index");
            }                        

            ViewBag.FuncionarioId = new SelectList(_funcionarioApp.buscaPorGerente(this.idUsuario), "FuncionarioId", "nome");
            ViewBag.QuestionarioId = new SelectList(_questionarioApp.GetAll(), "QuestionarioId", "descricao");

            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RelatorioGerente(FormCollection collection)
        {
            this.idUsuario = Convert.ToInt32(collection["FuncionarioId"]);

            DateTime dataInicio = Convert.ToDateTime("01/01/1900");
            DateTime dataFim = Convert.ToDateTime("31/12/2999");
            int? questionarioId = null;

            try
            {
                dataInicio = Convert.ToDateTime(collection["dataInicio"]);
            }
            catch
            {
                dataInicio = Convert.ToDateTime("01/01/1900");
            }

            try
            {
                dataFim = Convert.ToDateTime(collection["dataFim"]);
            }
            catch
            {
                dataFim = Convert.ToDateTime("31/12/2999");
            }

            try
            {
                questionarioId = Convert.ToInt32(collection["QuestionarioId"]);
            }
            catch
            {
                questionarioId = null;
            }

            List<QuestionarioViewModel> lista = retornaQuestionarios(idUsuario, dataInicio, dataFim, questionarioId);
            TempData["questionarios"] = lista;


            return RedirectToAction("VisualizaRelatorio");
        }
    }
}