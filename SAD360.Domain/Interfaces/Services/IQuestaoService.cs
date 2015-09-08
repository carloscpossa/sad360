using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Interfaces.Services
{
    public interface IQuestaoService : IServiceBase<Questao>
    {
        IEnumerable<Questao> buscaPorQuestionarioId(int idQuestionario);

        void incluiQuestao(Questao questao, Alternativa alternativa1, Alternativa alternativa2, Alternativa alternativa3, Alternativa alternativa4, Alternativa alternativa5);
    }
}
