using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Entities
{
    public class Questao
    {
        public int QuestaoId { get; set; }

        public string texto { get; set; }

        public int QuestionarioId { get; set; }

        public virtual Questionario questionario { get; set; }

        public virtual ICollection<Alternativa> alternativas { get; set; }
        
    }
}
