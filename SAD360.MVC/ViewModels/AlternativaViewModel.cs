using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAD360.MVC.ViewModels
{
    public class AlternativaViewModel
    {
        [Key]
        public int AlternativaId { get; set; }

        [Required(ErrorMessage="Texto da alternativa é obrigatório")]
        [MaxLength(200, ErrorMessage="Tamanho máximo de {0} caracteres")]
        [Display(Name="Texto da Alternativa")]
        public string texto { get; set; }

        public int QuestaoId { get; set; }

        public virtual QuestaoViewModel questao { get; set; }

        public virtual ICollection<AvaliacaoViewModel> avaliacoes { get; set; }

        public double percentual { get; set; }
    }
}