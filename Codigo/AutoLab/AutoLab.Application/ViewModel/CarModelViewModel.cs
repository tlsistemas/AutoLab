using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Application.ViewModel
{
    public class CarModelViewModel
    {
        public string Key { get; set; }
        public string KeyCarBrand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool Removed { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
