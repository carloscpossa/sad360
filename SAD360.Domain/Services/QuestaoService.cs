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
    public class QuestaoService : ServiceBase<Questao>, IQuestaoService
    {
        private readonly IQuestaoRepository _questaoRepository;
        public QuestaoService(IQuestaoRepository questaoRepository)
            : base(questaoRepository)
        {
            _questaoRepository = questaoRepository;
        }

        public IEnumerable<Questao> buscaPorQuestionarioId(int idQuestionario)
        {
            return _questaoRepository.buscaPorQuestionarioId(idQuestionario);
        }


        public void incluiQuestao(Questao questao, Alternativa alternativa1, Alternativa alternativa2, Alternativa alternativa3, Alternativa alternativa4, Alternativa alternativa5)
        {
            _questaoRepository.incluiQuestao(questao, alternativa1, alternativa2, alternativa3, alternativa4, alternativa5);
        }
    }
}
