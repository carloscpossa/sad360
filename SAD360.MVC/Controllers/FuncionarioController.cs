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
    public class FuncionarioController : Controller
    {
        private int idUsuario = 0;
        private readonly IFuncionarioAppService _funcionarioApp;        
        public FuncionarioController(IFuncionarioAppService funcionarioApp)
        {
            _funcionarioApp = funcionarioApp;            
        }

        // GET: Funcionario
        public ActionResult Index()
        {
            IEnumerable<FuncionarioViewModel> funcionarios = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(_funcionarioApp.GetAll());
            return View(funcionarios);
        }

        // GET: Funcionario/Details/5
        public ActionResult Details(int id)
        {
            Funcionario func = _funcionarioApp.GetById(id);
            FuncionarioViewModel funcionario = Mapper.Map<Funcionario, FuncionarioViewModel>(func);
            if (func.GerenteId != null)
            {
                Funcionario gerente = _funcionarioApp.GetById(Convert.ToInt32(func.GerenteId));
                ViewBag.responsavel = gerente.nome;
            }
            else
            {
                ViewBag.responsavel = "";
            }
            return View(funcionario);
        }

        // GET: Funcionario/Create
        public ActionResult Create()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            ViewBag.GerenteId = new SelectList(_funcionarioApp.obterApenasGerenteOuAdministradores(_funcionarioApp.GetAll()), "FuncionarioId", "nome");
            return View();
        }

        // POST: Funcionario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionarioViewModel funcionario)
        {
            if (ModelState.IsValid)
            {
                funcionario.AdministradorId = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));

                if (funcionario.ehGerente)
                {
                    Gerente gerente = Mapper.Map<FuncionarioViewModel, Gerente>(funcionario);                   
                    _funcionarioApp.Add(gerente);
                    return RedirectToAction("Index");
                }
                
                if (funcionario.ehAdministrador)
                {
                    Administrador adm = Mapper.Map<FuncionarioViewModel, Administrador>(funcionario);                    
                    _funcionarioApp.Add(adm);
                    return RedirectToAction("Index");
                }


                Funcionario func = Mapper.Map<FuncionarioViewModel, Funcionario>(funcionario);
                _funcionarioApp.Add(func);
                return RedirectToAction("Index");
            }

            return View(funcionario);
        }

        // GET: Funcionario/Edit/5
        public ActionResult Edit(int id)
        {
            int idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            FuncionarioViewModel funcionario = Mapper.Map<Funcionario, FuncionarioViewModel>(_funcionarioApp.GetById(id));

            ViewBag.GerenteId = new SelectList(_funcionarioApp.obterApenasGerenteOuAdministradores(_funcionarioApp.GetAll()), "FuncionarioId", "nome", funcionario.GerenteId);
            return View(funcionario);
        }

        // POST: Funcionario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FuncionarioViewModel funcionario)
        {
            if (ModelState.IsValid)
            {                              
                Funcionario func = Mapper.Map<FuncionarioViewModel, Funcionario>(funcionario);
                _funcionarioApp.Update(func, funcionario.ehAdministrador, funcionario.ehGerente);
                return RedirectToAction("Index");
            }

            return View(funcionario);
        }

        // GET: Funcionario/Delete/5
        public ActionResult Delete(int id)
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            Funcionario func = _funcionarioApp.GetById(idUsuario);
            if (!func.ehAdministrador)
            {
                return View("AcessoNegado");
            }

            Funcionario funcionario = _funcionarioApp.GetById(id);

            if (funcionario != null)
            {                
                _funcionarioApp.Remove(funcionario);                
            }

            return RedirectToAction("Index");
        }

        public ActionResult AlteraSenha()
        {
            this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
            FuncionarioViewModel funcionario = Mapper.Map<Funcionario, FuncionarioViewModel>(_funcionarioApp.GetById(idUsuario));

            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlteraSenha(FormCollection f)
        {
            try
            {
                this.idUsuario = Convert.ToInt32(new Criptografia().Decrypt(User.Identity.Name));
                Funcionario funcionario = _funcionarioApp.GetById(idUsuario);

                funcionario.senha = f["senha"];
                _funcionarioApp.Update(funcionario);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
