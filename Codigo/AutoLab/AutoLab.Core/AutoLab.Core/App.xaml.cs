using AutoLab.Core.Services;
using AutoLab.Core.Services.Interfaces;
using AutoLab.Core.ViewModels;
using AutoLab.Services;
using AutoLab.Services.Interfaces;
using Xamarin.Forms;

namespace AutoLab.Core
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            RegisterServices();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #region Metodos
        void RegisterServices()
        {
            DependencyService.Register<IApiService, ApiService>();
            DependencyService.Register<ICarBrandService, CarBrandService>();
        }
        #endregion
    }
}
