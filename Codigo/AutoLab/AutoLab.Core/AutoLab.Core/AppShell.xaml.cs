using AutoLab.Core.ViewModels;
using AutoLab.Core.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AutoLab.Core
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CarBrandPage), typeof(CarBrandPage));
            Routing.RegisterRoute(nameof(CarModelPage), typeof(CarModelPage));
        }
    }
}
