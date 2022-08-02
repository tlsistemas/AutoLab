﻿using System;
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
        public ObservableCollection<CarBrandViewModel> Items { get; }
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
            Title = "Modelos";
            Items = new ObservableCollection<CarBrandViewModel>();
            ItemTapped = new Command<CarBrandViewModel>(OnItemSelected);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        async void OnItemSelected(CarBrandViewModel item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
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