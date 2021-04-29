using AutarkyBudget.Models;
using AutarkyBudget.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; }
        public Command AddItemCommand { get; }
        public Command DeleteItemCommand { get; }
        public Command EditItemCommand { get; }

        public ItemsViewModel()
        {
            // Bindings.
            Title = "Budget";
            Items = new ObservableCollection<Item>();

            // Commands.
            AddItemCommand    = new Command(OnAddItem);
            DeleteItemCommand = new Command(async () => await LoadItems());
            EditItemCommand   = new Command(async () => await LoadItems());
        }

        private async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true).ConfigureAwait(false);
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

        public async Task OnAppearing()
        {
            await LoadItems().ConfigureAwait(false);
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
    }
}
