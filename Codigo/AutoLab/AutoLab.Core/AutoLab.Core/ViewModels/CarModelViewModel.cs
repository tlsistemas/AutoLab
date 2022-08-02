using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Core.ViewModels
{
    public class CarModelViewModel
    {
        public string Key { get; set; }
        public CarBrandViewModel CarBrand { get; set; }
        public string CarBrandKey { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool Removed { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public CarModelViewModel()
        {
            CarBrand = new CarBrandViewModel();
        }
    }
}
