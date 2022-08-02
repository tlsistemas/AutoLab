using AutoLab.Application.ViewModel;
using AutoLab.Domain.Entities;
using AutoMapper;

namespace AutoLab.Application.AutoMapper
{
    public class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            CreateMap<CarBrandViewModel, CarBrand>();
            CreateMap<CarModelViewModel, CarModel>();
        }
    }
}
