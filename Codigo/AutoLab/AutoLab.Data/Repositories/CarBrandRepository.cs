using AutoLab.Data.Contexts;
using AutoLab.Domain.Entities;
using AutoLab.Domain.Interfaces.Repositories;
using AutoLab.Utils.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Data.Repositories
{
    public class CarBrandRepository : RepositoryBase<CarBrand>, ICarBrandRepository
    {
        public CarBrandRepository(AutoLabContext ctx) : base(ctx)
        {
        }
    }
}