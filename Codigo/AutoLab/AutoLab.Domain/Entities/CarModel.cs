using AutoLab.Utils.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Domain.Entities
{
    public class CarModel:EntityBase
    {
        public int IdCarBrand { get; set; }
        public CarBrand CarBrand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
