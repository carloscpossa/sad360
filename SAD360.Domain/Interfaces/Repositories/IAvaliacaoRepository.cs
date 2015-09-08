using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Interfaces.Repositories
{
    public interface IAvaliacaoRepository:IRepositoryBase<Avaliacao>
    {
        IEnumerable<Avaliacao> buscaPorAvaliadorId(int id);

        void SalvarRespostas(Avaliacao avaliacao);

        IEnumerable<Avaliacao> pesquisa(int avaliadoId, DateTime dataInicioPreenchimento, DateTime dataTerminoPreenchimento, int? questionarioId);

        IEnumerable<Avaliacao> buscaAvaliacaoSemPreenchimento();
    }
}
