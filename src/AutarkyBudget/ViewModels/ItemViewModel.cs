using AutarkyBudget.Models;
using AutarkyBudget.Repository.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        #region Properties

        private readonly IItemRepository _itemRepository;

        private DateTimeOffset? _knownTime = null;

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;

        public string Amount { get => _amount; set => SetProperty(ref _amount, value); }
        private string _amount;

        public string ItemId { get => _itemId; set => SetProperty(ref _itemId, value); }
        private string _itemId;

        #endregion

        #region ICommand

        public Command SaveCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Constructors

        public ItemViewModel()
        {
            // Services.
            _itemRepository = DependencyService.Get<IItemRepository>();

            // Bindings.
            Title = "New";

            // Commands.
            SaveCommand   = new Command(ExecuteSave, ValidateSave);
            CancelCommand = new Command(ExecuteCancel);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        #endregion

        #region Methods

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_name)
                && !string.IsNullOrWhiteSpace(_amount);
        }

        private async void ExecuteCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void ExecuteSave()
        {
            var item = new Item()
            {
                Id = string.IsNullOrEmpty(ItemId) ? Guid.NewGuid().ToString() : ItemId,
                CreationTime = _knownTime ?? DateTimeOffset.Now,
                Name = Name,
                Amount = Amount,
            };

            _itemRepository.Upsert(item);

            await Shell.Current.GoToAsync("..");
        }

        public void OnAppearing()
        {
            if (!string.IsNullOrEmpty(ItemId))
            {
                Item item = _itemRepository.Get(ItemId);
                _knownTime = item.CreationTime;
                Name = item.Name;
                Amount = item.Amount;
            }
        }

        #endregion
    }
}
