using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Entities
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        public string nome { get; set; }

        public string setor { get; set; }
        public string matricula { get; set; }
        public string senha { get; set; }

        public int? GerenteId { get; set; }

        public virtual Gerente responsavel { get; set; }

        public int? AdministradorId { get; set; }

        public virtual Administrador administradorCadastro { get; set; }

        public bool ehAdministrador
        {
            get
            {
                return (this is Administrador);
            }
        }

        public bool ehGerente
        {
            get
            {
                return (this is Gerente);
            }
        }
    }
}
