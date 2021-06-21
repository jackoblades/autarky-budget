using AutarkyBudget.Models.Domain;
using AutarkyBudget.Repository.Interfaces;
using AutarkyBudget.Views;
using NodaMoney;
using System.Collections.Generic;
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

        private readonly IBudgetItemRepository _itemRepository;

        public ObservableCollection<BudgetItem> Items { get; }

        public Money Total { get => _total; set => SetProperty(ref _total, value); }
        private Money _total = new Money();

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
            _itemRepository = DependencyService.Get<IBudgetItemRepository>();

            // Bindings.
            Title = "Budget";
            Items = new ObservableCollection<BudgetItem>();

            // Commands.
            AddItemCommand    = new Command(async () => await OnAddItemAsync());
            DeleteItemCommand = new Command<BudgetItem>((x) => DeleteItem(x));
            EditItemCommand   = new Command<BudgetItem>(async (x) => await EditItemAsync(x));
        }

        #endregion

        #region Methods

        private void LoadItems()
        {
            IsBusy = true;

            Items.Clear();
            IEnumerable<BudgetItem> items = _itemRepository.GetAll().OrderBy(x => x.CreationTime);
            foreach (BudgetItem item in items ?? Enumerable.Empty<BudgetItem>())
            {
                Items.Add(item);
            }
            Total = Items.SumOfValues();

            IsBusy = false;
        }

        private void DeleteItem(BudgetItem item)
        {
            IsBusy = true;

            Items.Remove(item);
            _ = _itemRepository.Remove(item);
            Total = Items.SumOfValues();

            IsBusy = false;
        }

        private async Task EditItemAsync(BudgetItem item)
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
