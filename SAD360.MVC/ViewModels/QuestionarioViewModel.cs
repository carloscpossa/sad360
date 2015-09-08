using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAD360.MVC.ViewModels
{
    public class QuestionarioViewModel
    {
        [Key]
        [Display(Name="Código")]
        public int QuestionarioId { get; set; }

        [Required(ErrorMessage="Descrição do questionário obrigatória.")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo de {0} caracteres")]
        [Display(Name="Descrição")]
        public string descricao { get; set; }

        public int AdministradorId { get; set; }

        public virtual AdministradorViewModel administrador { get; set; }

        public virtual IEnumerable<QuestaoViewModel> questoes { get; set; }

        public virtual IEnumerable<AvaliacaoViewModel> avaliacoes { get; set; }
    }
}