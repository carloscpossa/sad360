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
    public class AdministradorAppService : AppServiceBase<Administrador>, IAdministradorAppService
    {
        private readonly IAdministradorService _administradorService;

        public AdministradorAppService(IAdministradorService administradorService)
            : base(administradorService)
        {
            _administradorService = administradorService;
        }
    }
}
