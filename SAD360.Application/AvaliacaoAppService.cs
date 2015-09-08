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
    public class AvaliacaoAppService : AppServiceBase<Avaliacao>, IAvaliacaoAppService
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoAppService(IAvaliacaoService avaliacaoService)
            : base(avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        public void geraAvaliacao(DateTime dataInicio, DateTime dataFim, int questionario, int avaliado, int avaliador, int AdministradorId)
        {
            Avaliacao avaliacao = new Avaliacao();
            avaliacao.dataGeracao = DateTime.Today;
            avaliacao.dataInicio = dataInicio;
            avaliacao.dataTermino = dataFim;
            avaliacao.FuncionarioAvaliadoId = avaliado;
            avaliacao.FuncionarioAvaliadorId = avaliador;
            avaliacao.QuestionarioId = questionario;
            avaliacao.AdministradorId = AdministradorId;
            _avaliacaoService.Add(avaliacao);
        }


        public IEnumerable<Avaliacao> buscaPendentesPorAvaliadorId(int id)
        {
            return _avaliacaoService.buscaPendentesPorAvaliadorId(id);
        }


        public void SalvarRespostas(Avaliacao avaliacao)
        {
            _avaliacaoService.SalvarRespostas(avaliacao);
        }


        public IEnumerable<Avaliacao> pesquisa(int avaliadoId, DateTime dataInicioPreenchimento, DateTime dataTerminoPreenchimento, int? questionarioId)
        {
            return _avaliacaoService.pesquisa(avaliadoId, dataInicioPreenchimento, dataTerminoPreenchimento, questionarioId);
        }


        public IEnumerable<Avaliacao> buscaAvaliacoesPendentes()
        {
            return _avaliacaoService.buscaAvaliacoesPendentes();
        }
    }
}
