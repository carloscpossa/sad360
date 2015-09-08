using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAD360.MVC.ViewModels
{
    public class QuestaoViewModel
    {
        [Key]
        public int QuestaoId { get; set; }

        [Required(ErrorMessage="Descrição da questão obrigatória.")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [Display(Name = "Descrição")]
        public string texto { get; set; }

        public int QuestionarioId { get; set; }

        public virtual QuestionarioViewModel questionario { get; set; }

        public virtual ICollection<AlternativaViewModel> alternativas { get; set; }

        public QuestaoViewModel()
        {
            alternativas = new List<AlternativaViewModel>();
        }
    }
}