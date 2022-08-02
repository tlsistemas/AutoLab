using AutoLab.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Core.Services.Interfaces
{
    public interface ICarModelService
    {
        Task<IEnumerable<CarModelViewModel>> GetCarModel();
        Task<IEnumerable<CarModelViewModel>> GetCarModel(string carBrand);

    }
}
