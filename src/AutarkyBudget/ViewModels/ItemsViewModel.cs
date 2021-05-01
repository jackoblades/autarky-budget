using AutarkyBudget.Models;
using AutarkyBudget.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Item> Items { get; }

        public decimal Total { get => _total; set => SetProperty(ref _total, value); }
        private decimal _total = 0M;

        #endregion

        #region ICommand

        public ICommand AddItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand EditItemCommand { get; }

        #endregion

        #region Constructors

        public ItemsViewModel()
        {
            // Bindings.
            Title = "Budget";
            Items = new ObservableCollection<Item>();

            // Commands.
            AddItemCommand    = new Command(OnAddItemAsync);
            DeleteItemCommand = new Command<Item>(async (x) => await DeleteItemAsync(x));
            EditItemCommand   = new Command<Item>(async (x) => await DeleteItemAsync(x));
        }

        #endregion

        #region Methods

        private async Task LoadItemsAsync()
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
                Total = Items.SumOfValues();
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

        private async Task DeleteItemAsync(Item item)
        {
            IsBusy = true;

            try
            {
                Items.Remove(item);
                var items = await DataStore.DeleteItemAsync(item.Id).ConfigureAwait(false);
                Total = Items.SumOfValues();
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

        public async Task OnAppearingAsync()
        {
            await LoadItemsAsync().ConfigureAwait(false);
        }

        private async void OnAddItemAsync(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        #endregion
    }
}
