using SAD360.Domain.Entities;
using SAD360.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.Repositories
{
    public class AlternativaRepository : RepositoryBase<Alternativa>, IAlternativaRepository
    {
        public IEnumerable<Alternativa> buscaPorQuestaoId(int idQuestao)
        {
            return db.Set<Alternativa>().Where(a => a.QuestaoId == idQuestao);
        }
    }
}
