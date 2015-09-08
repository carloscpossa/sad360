using SAD360.Domain.Entities;
using SAD360.Domain.Interfaces.Repositories;
using SAD360.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Services
{
    public class FuncionarioService : ServiceBase<Funcionario>, IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
            : base(funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public Funcionario buscaPorMatriculaSenha(string matricula, string senha)
        {
            return _funcionarioRepository.buscaPorMatriculaSenha(matricula, senha);
        }


        public IEnumerable<Funcionario> obterApenasGerenteOuAdministradores(IEnumerable<Funcionario> funcionarios)
        {
            return funcionarios.Where(f => f.ehAdministrador | f.ehGerente);
        }


        public void Update(Funcionario obj, bool administrador, bool gerente)
        {
            _funcionarioRepository.Update(obj, administrador, gerente);
        }


        public IEnumerable<Funcionario> buscaPorGerente(int idGerente)
        {
            return _funcionarioRepository.buscaPorGerente(idGerente);
        }
    }
}
