using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Interfaces.Services
{
    public interface IAvaliacaoService : IServiceBase<Avaliacao>
    {
        IEnumerable<Avaliacao> buscaPendentesPorAvaliadorId(int id);

        void SalvarRespostas(Avaliacao avaliacao);

        IEnumerable<Avaliacao> pesquisa(int avaliadoId, DateTime dataInicioPreenchimento, DateTime dataTerminoPreenchimento, int? questionarioId);

        IEnumerable<Avaliacao> buscaAvaliacoesPendentes();
    }
}
