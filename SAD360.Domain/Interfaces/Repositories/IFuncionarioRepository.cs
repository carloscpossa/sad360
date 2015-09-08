using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Interfaces.Repositories
{
    public interface IFuncionarioRepository:IRepositoryBase<Funcionario>
    {
        Funcionario buscaPorMatriculaSenha(string matricula, string senha);

        void Update(Funcionario obj, bool administrador, bool gerente);

        IEnumerable<Funcionario> buscaPorGerente(int idGerente);
    }
}
