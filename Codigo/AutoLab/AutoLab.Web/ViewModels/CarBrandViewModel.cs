using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Web.ViewModels
{
    public class CarBrandViewModel
    {
        public string Key { get; set; }
        public string Brand { get; set; }
        public bool Removed { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
