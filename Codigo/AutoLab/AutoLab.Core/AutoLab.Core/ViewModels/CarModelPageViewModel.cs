using AutoLab.Core.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace AutoLab.Core.ViewModels
{
    public class CarModelPageViewModel : BaseViewModel, IQueryAttributable
    {
        private CarModelViewModel _selectedItem;
        public ObservableCollection<CarModelViewModel> Items { get; set; }
        public ICommand LoadItemsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await ExecuteLoadItemsCommand().ConfigureAwait(true);
                });
            }
        }
        public string BrandKey { get; set; }

        public CarModelViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }


        public CarModelPageViewModel()
        {
            Title = "Modelos";
            Items = new ObservableCollection<CarModelViewModel>();
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                if (BrandKey != null)
                {
                    Items.Clear();
                    var items = await CarModelService.GetCarModel(BrandKey);

                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            BrandKey = HttpUtility.UrlDecode(query["guid"]);
           await ExecuteLoadItemsCommand();
        }
    }
}
