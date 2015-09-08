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
    public class AvaliacaoService : ServiceBase<Avaliacao>, IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository)
            : base(avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public IEnumerable<Avaliacao> buscaPendentesPorAvaliadorId(int id)
        {
            IEnumerable<Avaliacao> avaliacoes = _avaliacaoRepository.buscaPorAvaliadorId(id);

            avaliacoes = avaliacoes.Where(a => a.avaliacaoPendente).ToList();
            
            return avaliacoes;
        }


        public void SalvarRespostas(Avaliacao avaliacao)
        {
            _avaliacaoRepository.SalvarRespostas(avaliacao);
        }


        public IEnumerable<Avaliacao> pesquisa(int avaliadoId, DateTime dataInicioPreenchimento, DateTime dataTerminoPreenchimento, int? questionarioId)
        {
            return _avaliacaoRepository.pesquisa(avaliadoId, dataInicioPreenchimento, dataTerminoPreenchimento, questionarioId);
        }


        public IEnumerable<Avaliacao> buscaAvaliacoesPendentes()
        {
            IEnumerable<Avaliacao> avaliacoes = _avaliacaoRepository.buscaAvaliacaoSemPreenchimento();
            avaliacoes = avaliacoes.Where(a => a.avaliacaoPendente).ToList();

            return avaliacoes;
        }
    }
}
