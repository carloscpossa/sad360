using SAD360.Domain.Entities;
using SAD360.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.Repositories
{
    public class QuestaoRepository : RepositoryBase<Questao>, IQuestaoRepository
    {
        public IEnumerable<Questao> buscaPorQuestionarioId(int idQuestionario)
        {            
            return db.Set<Questao>().Where(q => q.QuestionarioId == idQuestionario);
        }        

        public void incluiQuestao(Questao questao, Alternativa alternativa1, Alternativa alternativa2, Alternativa alternativa3, Alternativa alternativa4, Alternativa alternativa5)
        {
            db.Set<Questao>().Add(questao);

            if (alternativa1 != null)
            {
                alternativa1.QuestaoId = questao.QuestaoId;
                db.Set<Alternativa>().Add(alternativa1);
            }

            if (alternativa2 != null)
            {
                alternativa2.QuestaoId = questao.QuestaoId;
                db.Set<Alternativa>().Add(alternativa2);
            }

            if (alternativa3 != null)
            {
                alternativa3.QuestaoId = questao.QuestaoId;
                db.Set<Alternativa>().Add(alternativa3);
            }

            if (alternativa4 != null)
            {
                alternativa4.QuestaoId = questao.QuestaoId;
                db.Set<Alternativa>().Add(alternativa4);
            }

            if (alternativa5 != null)
            {
                alternativa5.QuestaoId = questao.QuestaoId;
                db.Set<Alternativa>().Add(alternativa5);
            }

            db.SaveChanges();
        }
    }
}
