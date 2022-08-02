using AutoLab.Domain.Entities;
using AutoLab.Domain.Interfaces.Repositories;
using AutoLab.Domain.Interfaces.Services;
using AutoLab.Utils.Bases;

namespace AutoLab.Domain.Services
{
    public class CarBrandService : ServiceBase<CarBrand>, ICarBrandService
    {
        public readonly ICarBrandRepository _repository;

        public CarBrandService(ICarBrandRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}