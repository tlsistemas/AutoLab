using AutoLab.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoLab.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Guid), "guid")]
    public partial class CarModelPage : ContentPage
    {
        public string Guid
        {
            set
            {
                // Custom logic
            }
        }
        public CarModelPage()
        {
            InitializeComponent();
            var viewModel = new CarModelPageViewModel();
            BindingContext = viewModel;
            viewModel.OnAppearing();
        }
    }
}