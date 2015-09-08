using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAD360.MVC.ViewModels
{
    public class FuncionarioViewModel
    {

        [Key]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = "Nome do funcionário obrigatório")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Departamento")]
        public string setor { get; set; }

        [Required(ErrorMessage = "Matrícula do funcionário obrigatória")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Matrícula")]
        public string matricula { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string senha { get; set; }


        [Required(ErrorMessage = "Confirmação de Senha obrigatória")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Confirmação da Senha")]
        [DataType(DataType.Password)]
        [Compare("senha",ErrorMessage="A confirmação da senha está diferente da senha informada")]
        public string confirmaSenha { get; set; }


        [Display(Name = "Responsável")]
        public int? GerenteId { get; set; }

        public virtual GerenteViewModel responsavel { get; set; }

        public int? AdministradorId { get; set; }

        public virtual AdministradorViewModel administradorCadastro { get; set; }

        [Display(Name="Administrador")]
        public bool ehAdministrador { get; set; }

        [Display(Name = "Gerente")]
        public bool ehGerente { get; set; }
    }
}