using AutoLab.Core.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AutoLab.Core.ViewModels
{
    public class CarBrandPageViewModel : BaseViewModel
    {
        private CarBrandViewModel _selectedItem;
        public ObservableCollection<CarBrandViewModel> Items { get; set; }
        public Command<CarBrandViewModel> ItemTapped { get; }
        public Command LoadItemsCommand { get; }

        public CarBrandViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }


        public CarBrandPageViewModel()
        {
            Title = "Brands";
            Items = new ObservableCollection<CarBrandViewModel>();
            ItemTapped = new Command<CarBrandViewModel>(OnItemSelected);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        async void OnItemSelected(CarBrandViewModel item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CarModelPage)}?guid={item.Key}");
            //await Shell.Current.GoToAsync("CarModelPage?Data=from page2");
            //await Shell.Current.GoToAsync($"CarModelPage?Brand={item.Key}");
            //await Shell.Current.GoToAsync($"{nameof(CarModelPage)}");
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await CarBrandService.GetCarBrand();

                foreach (var item in items)
                {
                    Items.Add(item);
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
    }
}
