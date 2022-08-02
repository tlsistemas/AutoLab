using AutoLab.Application.Interfaces;
using AutoLab.Application.Parameters;
using AutoLab.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoLab.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICarBrandApplication _pageApplication;

        public IEnumerable<CarBrandViewModel> Models;

        public IndexModel(ILogger<IndexModel> logger,
            ICarBrandApplication PageApplication)
        {
            _logger = logger;
            _pageApplication = PageApplication;
        }

        public async void OnGet()
        {
            var itens = await _pageApplication.ListCarBrandAsync();
            if (!itens.Error)
                Models = itens.Data;
            else
                Models = new List<CarBrandViewModel>();
        }
    }
}