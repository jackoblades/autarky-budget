using AutarkyBudget.Models;
using AutarkyBudget.Repository.Interfaces;
using AutarkyBudget.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Properties

        private readonly IItemRepository _itemRepository;

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
            // Services.
            _itemRepository = DependencyService.Get<IItemRepository>();

            // Bindings.
            Title = "Budget";
            Items = new ObservableCollection<Item>();

            // Commands.
            AddItemCommand    = new Command(async () => await OnAddItemAsync());
            DeleteItemCommand = new Command<Item>((x) => DeleteItem(x));
            EditItemCommand   = new Command<Item>(async (x) => await EditItemAsync(x));
        }

        #endregion

        #region Methods

        private void LoadItems()
        {
            IsBusy = true;

            Items.Clear();
            var items = _itemRepository.GetAll();
            foreach (var item in items ?? Enumerable.Empty<Item>())
            {
                Items.Add(item);
            }
            Total = Items.SumOfValues();

            IsBusy = false;
        }

        private void DeleteItem(Item item)
        {
            IsBusy = true;

            Items.Remove(item);
            _ = _itemRepository.Remove(item);
            Total = Items.SumOfValues();

            IsBusy = false;
        }

        private async Task EditItemAsync(Item item)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemPage)}?id={item.Id}");
        }

        public void OnAppearing()
        {
            LoadItems();
        }

        private async Task OnAddItemAsync()
        {
            await Shell.Current.GoToAsync(nameof(ItemPage));
        }

        #endregion
    }
}
