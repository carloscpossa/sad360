using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAD360.MVC.ViewModels
{
    public class AdministradorViewModel
    {
        public virtual IEnumerable<FuncionarioViewModel> funcionariosCadastrados { get; set; }
    }
}