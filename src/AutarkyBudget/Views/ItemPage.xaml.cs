using AutarkyBudget.ViewModels;
using Xamarin.Forms;

namespace AutarkyBudget.Views
{
    [QueryProperty(nameof(ItemId), "id")]
    public partial class ItemPage : PageBase
    {
        #region Properties

        private readonly ItemViewModel _vm;

        public string ItemId
        {
            set => _vm.ItemId = value;
        }

        #endregion

        #region Constructors

        public ItemPage()
        {
            InitializeComponent();

            BindingContext = _vm = new ItemViewModel();
        }

        #endregion

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.OnAppearing();
        }

        #endregion
    }
}
