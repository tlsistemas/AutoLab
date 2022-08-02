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
    public partial class CarBrandPage : ContentPage
    {
        public CarBrandPage()
        {
            InitializeComponent();
            var viewModel = new CarBrandPageViewModel();
            BindingContext = viewModel;
            viewModel.OnAppearing();
        }
    }
}