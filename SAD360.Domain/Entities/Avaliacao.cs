using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Domain.Entities
{
    public class Avaliacao
    {
        public int AvaliacaoId { get; set; }

        public DateTime dataGeracao { get; set; }

        public DateTime dataInicio { get; set; }

        public DateTime dataTermino { get; set; }

        public DateTime? dataPreenchimento { get; set; }

        public int FuncionarioAvaliadorId { get; set; }

        public virtual Funcionario funcionarioAvaliador { get; set; }

        public int FuncionarioAvaliadoId { get; set; }

        public virtual Funcionario funcionarioAvaliado { get; set; }

        public int QuestionarioId { get; set; }

        public virtual Questionario questionario { get; set; }

        public int AdministradorId { get; set; }

        public virtual Administrador administrador { get; set; }

        public virtual ICollection<Alternativa> respostas { get; set; }

        public bool avaliacaoPendente 
        {
            get
            {
                return (DateTime.Today >= this.dataInicio && DateTime.Today <= this.dataTermino && this.dataPreenchimento == null);
            }        
        }
    }
}
