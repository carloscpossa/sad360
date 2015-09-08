using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Entities
{
    public class Alternativa
    {
        public int AlternativaId { get; set; }

        public string texto { get; set; }

        public int QuestaoId { get; set; }

        public virtual Questao questao { get; set; }

        public virtual ICollection<Avaliacao> avaliacoes { get; set; }
    }
}
