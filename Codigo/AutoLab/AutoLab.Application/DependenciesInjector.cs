using AutoLab.Application.Application;
using AutoLab.Application.AutoMapper;
using AutoLab.Application.Interfaces;
using AutoLab.Data.Repositories;
using AutoLab.Domain.Interfaces.Repositories;
using AutoLab.Domain.Interfaces.Services;
using AutoLab.Domain.Services;
using AutoLab.Utils.Bases;
using AutoLab.Utils.Bases.Interface;
using AutoLab.Utils.Http;
using AutoLab.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoLab.Application
{
    public class DependenciesInjector
    {
        public static void Register(IServiceCollection svcCollection)
        {
            svcCollection.AddAutoMapper(typeof(ModelToViewModel), typeof(ViewModelToModel));

            #region Api
            svcCollection.AddScoped<IApiService, ApiService>();
            #endregion

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
