using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Entities
{
    public class Questionario
    {
        public int QuestionarioId { get; set; }

        public string descricao { get; set; }

        public int AdministradorId { get; set; }

        public virtual Administrador administrador { get; set; }

        public virtual ICollection<Questao> questoes { get; set; }

        public virtual ICollection<Avaliacao> avaliacoes { get; set; }
    }
}
