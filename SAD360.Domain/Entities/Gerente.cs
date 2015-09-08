using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Entities
{
    public class Gerente:Funcionario
    {
        public virtual ICollection<Funcionario> funcionariosSubordinados { get; set; }
    }
}
