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
    public class AlternativaService : ServiceBase<Alternativa>, IAlternativaService
    {
        private readonly IAlternativaRepository _alternativaRepository;

        public AlternativaService(IAlternativaRepository alternativaRepository)
            : base(alternativaRepository)
        {
            _alternativaRepository = alternativaRepository;
        }

        public IEnumerable<Alternativa> buscaPorQuestaoId(int idQuestao)
        {
            return _alternativaRepository.buscaPorQuestaoId(idQuestao);
        }
    }
}
