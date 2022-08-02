using AutoLab.Domain.Interfaces.Repositories;
using AutoLab.Domain.Entities;
using AutoLab.Utils.Bases;
using AutoLab.Domain.Interfaces.Services;

namespace AutoLab.Domain.Services
{
    public class CarModelService : ServiceBase<CarModel>, ICarModelService
    {
        public readonly ICarModelRepository _repository;

        public CarModelService(ICarModelRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}