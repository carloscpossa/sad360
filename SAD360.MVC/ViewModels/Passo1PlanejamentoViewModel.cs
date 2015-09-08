using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAD360.MVC.ViewModels
{
    public class Passo1PlanejamentoViewModel
    {
        [Required(ErrorMessage="Data obrigatória")]
        [DataType(DataType.Date,ErrorMessage="Data inválida")]
        [Display(Name="Início das Avaliações")]
        public DateTime? dataInicioPlanejamento { get; set; }

        [Required(ErrorMessage = "Data obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Data inválida")]
        [Display(Name = "Término das Avaliações")]        
        public DateTime? dataFimPlanejamento { get; set; }

        public IEnumerable<QuestionarioViewModel> questionarios { get; set; }
    }
}