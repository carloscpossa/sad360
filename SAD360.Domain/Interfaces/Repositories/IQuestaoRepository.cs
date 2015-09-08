using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Interfaces.Repositories
{
    public interface IQuestaoRepository:IRepositoryBase<Questao>
    {
        void incluiQuestao(Questao questao, Alternativa alternativa1, Alternativa alternativa2, Alternativa alternativa3, Alternativa alternativa4, Alternativa alternativa5);

        IEnumerable<Questao> buscaPorQuestionarioId(int idQuestionario);
    }
}
