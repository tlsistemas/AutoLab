using AutoLab.Core.Models.Response;
using AutoLab.Core.Navigation;
using AutoLab.Core.Services.Interfaces;
using AutoLab.Core.ViewModels;
using AutoLab.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AutoLab.Core.Services
{
    public class CarModelService : ICarModelService
    {
        private readonly IApiService api;
        public CarModelService()
        {
            api = DependencyService.Get<IApiService>();
        }

        public async Task<IEnumerable<CarModelViewModel>> GetCarModel()
        {
            try
            {
                var response = new BaseResponse<IEnumerable<CarModelViewModel>>();
                string json = await api.GetClientAsync(LinksApi.ListCarBrand);
                response = JsonConvert.DeserializeObject<BaseResponse<IEnumerable<CarModelViewModel>>>(json);

                if (!response.Error)
                    return response.Data;
                else
                    return new List<CarModelViewModel>();
            }
            catch (Exception ex)
            {
                return new List<CarModelViewModel>();
            }
        }

        public async Task<IEnumerable<CarModelViewModel>> GetCarModel(string carBrand)
        {
            try
            {
                var response = new BaseResponse<IEnumerable<CarModelViewModel>>();
                string json = await api.GetClientAsync(LinksApi.ListCarModel + $"?KeyCarBrand={carBrand}");
                response = JsonConvert.DeserializeObject<BaseResponse<IEnumerable<CarModelViewModel>>>(json);

                if (!response.Error)
                    return response.Data;
                else
                    return new List<CarModelViewModel>();
            }
            catch (Exception ex)
            {
                return new List<CarModelViewModel>();
            }
        }
    }
}
