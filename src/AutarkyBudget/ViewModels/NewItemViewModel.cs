using AutarkyBudget.Models;
using AutarkyBudget.Repository.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        #region Properties

        private readonly IItemRepository _itemRepository;

        public string Text { get => _text; set => SetProperty(ref _text, value); }
        private string _text;

        public string Description { get => _description; set => SetProperty(ref _description, value); }
        private string _description;

        #endregion

        #region ICommand

        public Command SaveCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Constructors

        public NewItemViewModel()
        {
            // Services.
            _itemRepository = DependencyService.Get<IItemRepository>();

            // Bindings.
            Title = "New";

            // Commands.
            SaveCommand   = new Command(ExecuteSave, ValidateSave);
            CancelCommand = new Command(ExecuteCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        #endregion

        #region Methods

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_text)
                && !string.IsNullOrWhiteSpace(_description);
        }

        private async void ExecuteCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void ExecuteSave()
        {
            var item = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Text,
                Amount = Description,
            };

            _itemRepository.Add(item);

            await Shell.Current.GoToAsync("..");
        }

        #endregion
    }
}
