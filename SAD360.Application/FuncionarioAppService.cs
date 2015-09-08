using SAD360.Application.Interfaces;
using SAD360.Domain.Entities;
using SAD360.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Application
{
    public class FuncionarioAppService : AppServiceBase<Funcionario>, IFuncionarioAppService
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioAppService(IFuncionarioService funcionarioService)
            : base(funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        public Funcionario buscaPorMatriculaSenha(string matricula, string senha)
        {
            return _funcionarioService.buscaPorMatriculaSenha(matricula, senha);
        }


        public IEnumerable<Funcionario> obterApenasGerenteOuAdministradores(IEnumerable<Funcionario> funcionarios)
        {
            return _funcionarioService.obterApenasGerenteOuAdministradores(funcionarios);
        }


        public void Update(Funcionario obj, bool administrador, bool gerente)
        {
            _funcionarioService.Update(obj, administrador, gerente);
        }


        public IEnumerable<Funcionario> buscaPorGerente(int idGerente)
        {
            return _funcionarioService.buscaPorGerente(idGerente);
        }
    }
}
