using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Properties

        public string Id { get; set; }

        public string Text { get => _text; set => SetProperty(ref _text, value); }
        private string _text;

        public string Description { get => _description; set => SetProperty(ref _description, value); }
        private string _description;

        public string ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
                LoadItemId(value);
            }
        }
        private string _itemId;

        #endregion

        #region Constructors

        public ItemDetailViewModel()
        {
        }

        #endregion

        #region Methods

        public void OnAppearing()
        {
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Name;
                Description = item.Amount;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        #endregion
    }
}
