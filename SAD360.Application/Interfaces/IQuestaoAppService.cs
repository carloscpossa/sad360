using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Application.Interfaces
{
    public interface IQuestaoAppService : IAppServiceBase<Questao>
    {
        IEnumerable<Questao> buscaPorQuestionarioId(int idQuestionario);

        void incluiQuestao(int quetionarioId, string textoQuestao, string alternativa1, string alternativa2, string alternativa3, string alternativa4, string alternativa5);

        void alteraQuestao(int questaoId, string textoQuestao, string alternativa1, string alternativa2, string alternativa3, string alternativa4, string alternativa5);
    }
}
