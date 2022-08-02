using AutoLab.Application.Application;
using AutoLab.Application.Interfaces;
using AutoLab.Data.Repositories;
using AutoLab.Domain.Interfaces.Repositories;
using AutoLab.Domain.Interfaces.Services;
using AutoLab.Domain.Services;
using AutoLab.Utils.Bases;
using AutoLab.Utils.Bases.Interface;
using C2P.Application.Application;
using C2P.Application.Interfaces;
using C2P.Data.Repositories;
using C2P.Domain.Interfaces.Repositories;
using C2P.Domain.Interfaces.Services;
using C2P.Domain.Services;
using C2P.FincsData;
using C2P.FincsData.Interfaces;
using C2P.Utils.Bases;
using C2P.Utils.Bases.Interface;
using C2P.Utils.Cryptography;
using C2P.Utils.Email;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Application
{
    public class DependenciesInjector
    {
        public static void Register(IServiceCollection svcCollection)
        {

            #region Application
            svcCollection.AddScoped<ICarBrandApplication, CarBrandApplication>();
            svcCollection.AddScoped<ICarModelApplication, CarModelApplication>();
            #endregion

            #region Domain
            svcCollection.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            svcCollection.AddScoped(typeof(ICarBrandService), typeof(CarBrandService));
            svcCollection.AddScoped(typeof(ICarModelService), typeof(CarModelService));
            #endregion

            #region Repository
            svcCollection.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            svcCollection.AddScoped(typeof(ICarBrandRepository), typeof(CarBrandRepository));
            svcCollection.AddScoped(typeof(ICarModelRepository), typeof(CarModelRepository));

            #endregion

        }
    }
}
