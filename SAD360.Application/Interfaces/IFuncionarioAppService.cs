using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Application.Interfaces
{
    public interface IFuncionarioAppService : IAppServiceBase<Funcionario>
    {
        Funcionario buscaPorMatriculaSenha(string matricula, string senha);

        IEnumerable<Funcionario> obterApenasGerenteOuAdministradores(IEnumerable<Funcionario> funcionarios);

        void Update(Funcionario obj, bool administrador, bool gerente);

        IEnumerable<Funcionario> buscaPorGerente(int idGerente);
    }
}
