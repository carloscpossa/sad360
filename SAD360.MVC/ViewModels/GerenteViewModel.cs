using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAD360.MVC.ViewModels
{
    public class GerenteViewModel
    {
        public virtual IEnumerable<FuncionarioViewModel> funcionariosSubordinados { get; set; }
    }
}