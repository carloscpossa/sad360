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
    public class GerenteService : ServiceBase<Gerente>, IGerenteService
    {
        private readonly IGerenteRepository _gerenteRepositoy;

        public GerenteService(IGerenteRepository gerenteRepositoy)
            :base(gerenteRepositoy)
        {
            _gerenteRepositoy = gerenteRepositoy;
        }
    }
}
