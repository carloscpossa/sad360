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
    public class PlanejaAvaliacaoController : Controller
    {
        private int idUsuario = 0;        
        private readonly IFuncionarioAppService _funcionarioApp;
        private readonly IQuestionarioAppService _questionarioApp;
        private readonly IAvaliacaoAppService _avaliacaoApp;

        public PlanejaAvaliacaoController(IFuncionarioAppService funcionarioApp, IQuestionarioAppService questionarioApp, IAvaliacaoAppService avaliacaoApp)
        {
            _funcionarioApp = funcionarioApp;
            _questionarioApp = questionarioApp;
            _avaliacaoApp = avaliacaoApp;
        }


        //Get
        public ActionResult Passo1()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            IEnumerable<QuestionarioViewModel> quetionarios = Mapper.Map<IEnumerable<Questionario>, IEnumerable<QuestionarioViewModel>>(_questionarioApp.GetAll());
            Passo1PlanejamentoViewModel passo1 = new Passo1PlanejamentoViewModel();            
            passo1.questionarios = quetionarios;

            return View(passo1);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Passo1(FormCollection collection)
        {            
            IEnumerable<QuestionarioViewModel> quetionarios = Mapper.Map<IEnumerable<Questionario>, IEnumerable<QuestionarioViewModel>>(_questionarioApp.GetAll());
            Passo1PlanejamentoViewModel passo1 = new Passo1PlanejamentoViewModel();            
            passo1.questionarios = quetionarios;

            DateTime dataIni = DateTime.Today;
            DateTime dataFim = DateTime.Today;

            try
            {
                dataIni = Convert.ToDateTime(collection["dataInicioPlanejamento"]);
            }
            catch
            {
                ModelState.AddModelError("dataInicioPlanejamento", "A data de início do planejamento está inválida.");
                return View(passo1);
            }

            try
            {
                dataFim = Convert.ToDateTime(collection["dataFimPlanejamento"]);
            }
            catch
            {
                ModelState.AddModelError("dataInicioPlanejamento", "A data de término do planejamento está inválida.");
                return View(passo1);
            }

            
            if (dataIni > dataFim)
            {
                ModelState.AddModelError("dataFimPlanejamento", "A data de término do planejamento não pode ser menor que a data de início.");
                passo1.dataInicioPlanejamento = dataIni;
                passo1.dataFimPlanejamento = dataFim;
                return View(passo1);
            }

            if (collection["questionario"] == null)
            {
                ModelState.AddModelError("questionarios", "Favor selecionar um questionário.");
                passo1.dataInicioPlanejamento = dataIni;
                passo1.dataFimPlanejamento = dataFim;
                return View(passo1);
            }

            TempData["dataIni"] = dataIni;
            TempData["dataFim"] = dataFim;
            TempData["questionario"] = collection["questionario"];

            return RedirectToAction("Passo2");
        }

        //Get
        public ActionResult Passo2()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            TempData.Keep("dataIni");
            TempData.Keep("dataFim");
            TempData.Keep("questionario");

            if (TempData["dataIni"] == null | TempData["dataFim"] == null | TempData["questionario"] == null)
            {
                return RedirectToAction("Passo1");
            }

            QuestionarioViewModel questionario = Mapper.Map<Questionario, QuestionarioViewModel>(_questionarioApp.GetById(Convert.ToInt32(TempData["questionario"])));

            return View(questionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Passo2(FormCollection collection)
        {
            return RedirectToAction("Passo3");
        }

        //Get
        public ActionResult Passo3()
        {

            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            TempData.Keep("dataIni");
            TempData.Keep("dataFim");
            TempData.Keep("questionario");

            if (TempData["dataIni"] == null | TempData["dataFim"] == null | TempData["questionario"] == null)
            {
                return RedirectToAction("Passo1");
            }

            IEnumerable<FuncionarioViewModel> funcionarios = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(_funcionarioApp.GetAll());

            return View(funcionarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Passo3(FormCollection collection)
        {
            List<Funcionario> avaliados = new List<Funcionario>();

            for (int i = 1; i <= collection.Count - 1; i++)
            {
                Funcionario func = _funcionarioApp.GetById(Convert.ToInt32(collection[i]));
                avaliados.Add(func);
            }

            TempData["avaliados"] = avaliados;

            return RedirectToAction("Passo4");
        }

        //Get
        public ActionResult Passo4()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            TempData.Keep("dataIni");
            TempData.Keep("dataFim");
            TempData.Keep("questionario");
            //TempData.Keep("avaliados");

            if (TempData["dataIni"] == null | TempData["dataFim"] == null | TempData["questionario"] == null | TempData["avaliados"] == null)
            {
                return RedirectToAction("Passo1");
            }

            ViewBag.avaliados = TempData["avaliados"];

            IEnumerable<FuncionarioViewModel> avaliadores = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(_funcionarioApp.GetAll());

            return View(avaliadores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Passo4(FormCollection collection)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            DateTime dataInicio = Convert.ToDateTime(TempData["dataIni"]);
            DateTime dataTermino = Convert.ToDateTime(TempData["dataFim"]);
            int questionario = Convert.ToInt32(TempData["questionario"]);
                         
            string separador = "-";
            for (int i = 1; i <= collection.Count - 1; i++)
            {
                string[] avaliadoAvaliador = collection[i].Split(separador[0]);

                _avaliacaoApp.geraAvaliacao(dataInicio, dataTermino, questionario, Convert.ToInt32(avaliadoAvaliador[0]), Convert.ToInt32(avaliadoAvaliador[1]), this.idUsuario);
            }

            TempData.Remove("dataIni");
            TempData.Remove("dataFim");
            TempData.Remove("questionario");

            return View("sucesso");
        }

    }
}