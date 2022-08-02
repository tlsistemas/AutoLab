using AutoLab.Application.ViewModel;
using AutoLab.Utils.Http.Response;
using AutoLab.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AutoLab.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;
        private readonly IApiService _apiService;

        public IEnumerable<CarModelViewModel> Models;

        public DetailsModel(ILogger<IndexModel> logger,
            IApiService apiService,
            IConfiguration configuration)
        {
            _logger = logger;
            _apiService = apiService;
            _configuration = configuration;
        }

        public async Task OnGet(string brand)
        {
            try
            {
                var url = _configuration.GetSection("UrlApi").Value;
                var json = await _apiService.GetClientAsync(url + $"/api/CarModel?KeyCarBrand={brand}&Include=CarBrand");
                var  response = JsonConvert.DeserializeObject<BaseResponse<IEnumerable<CarModelViewModel>>>(json);
                if (response != null && !response.Error)
                {
                    Models = response.Data;
                }
                else
                    Models = new List<CarModelViewModel>();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
