using AutoLab.Data.Contexts;
using AutoLab.Domain.Entities;
using AutoLab.Domain.Interfaces.Repositories;
using AutoLab.Utils.Bases;

namespace AutoLab.Data.Repositories
{
    public class CarModelRepository : RepositoryBase<CarModel>, ICarModelRepository
    {
        public CarModelRepository(AutoLabContext ctx) : base(ctx)
        {
        }
    }
}