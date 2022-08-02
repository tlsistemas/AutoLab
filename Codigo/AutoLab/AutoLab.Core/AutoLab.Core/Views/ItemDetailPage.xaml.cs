using AutoLab.Core.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AutoLab.Core.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}