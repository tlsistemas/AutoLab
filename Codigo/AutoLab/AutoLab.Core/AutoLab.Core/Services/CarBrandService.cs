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
    public class CarBrandService : ICarBrandService
    {
        private readonly IApiService api;
        public CarBrandService()
        {
            api = DependencyService.Get<IApiService>();
        }

        public async Task<IEnumerable<CarBrandViewModel>> GetCarBrand()
        {
            try
            {
                var response = new BaseResponse<IEnumerable<CarBrandViewModel>>();
                string json = await api.GetClientAsync(LinksApi.ListCarBrand);
                response = JsonConvert.DeserializeObject<BaseResponse<IEnumerable<CarBrandViewModel>>>(json);

                if (!response.Error)
                    return response.Data;
                else
                    return new List<CarBrandViewModel>();
            }
            catch (Exception ex)
            {
                return new List<CarBrandViewModel>();
            }
        }
    }
}
