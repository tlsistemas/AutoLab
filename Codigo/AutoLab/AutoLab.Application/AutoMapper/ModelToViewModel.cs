using AutoLab.Application.ViewModel;
using AutoLab.Domain.Entities;
using AutoMapper;

namespace AutoLab.Application.AutoMapper
{
    public class ModelToViewModel : Profile
    {
        public ModelToViewModel()
        {
            CreateMap<CarBrand, CarBrandViewModel>();
            CreateMap<CarModel, CarModelViewModel>();
        }
    }
}
