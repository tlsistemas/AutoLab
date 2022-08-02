using AutoLab.Core.Models.Response;
using AutoLab.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Core.Services.Interfaces
{
    public interface ICarBrandService
    {
        Task<IEnumerable<CarBrandViewModel>> GetCarBrand();
    }
}
