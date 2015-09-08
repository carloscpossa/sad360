using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAD360.MVC.ViewModels
{
    public class AvaliacaoViewModel
    {
        [Key]
        public int AvaliacaoId { get; set; }

        public DateTime dataGeracao { get; set; }

        [Display(Name="Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dataInicio { get; set; }

        [Display(Name = "Término")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dataTermino { get; set; }

        public DateTime? dataPreenchimento { get; set; }

        [Display(Name="Avaliador")]
        [Required(ErrorMessage="Favor informar o avaliador")]
        public int FuncionarioAvaliadorId { get; set; }

        public virtual FuncionarioViewModel funcionarioAvaliador { get; set; }

        [Display(Name = "Avaliado")]
        [Required(ErrorMessage = "Favor informar o avaliado")]
        public int FuncionarioAvaliadoId { get; set; }

        public virtual FuncionarioViewModel funcionarioAvaliado { get; set; }

        [Display(Name = "Questionário")]
        [Required(ErrorMessage = "Favor informar o questionário")]
        public int QuestionarioId { get; set; }

        public int AdministradorId { get; set; }

        public virtual AdministradorViewModel administrador { get; set; }

        public virtual QuestionarioViewModel questionario { get; set; }
        public virtual ICollection<AlternativaViewModel> respostas { get; set; }
    }
}