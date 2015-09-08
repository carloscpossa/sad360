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
    public class AlternativaAppService : AppServiceBase<Alternativa>, IAlternativaAppService
    {
        private readonly IAlternativaService _alternativaService;

        public AlternativaAppService(IAlternativaService alternativaService)
            : base(alternativaService)
        {
            _alternativaService = alternativaService;
        }

        public IEnumerable<Alternativa> buscaPorQuestaoId(int idQuestao)
        {
            return _alternativaService.buscaPorQuestaoId(idQuestao);
        }
    }
}
