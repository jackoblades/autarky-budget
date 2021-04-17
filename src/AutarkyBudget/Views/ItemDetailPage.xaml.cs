using AutarkyBudget.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutarkyBudget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        #region Properties

        ItemDetailViewModel _vm;

        #endregion

        #region Constructors

        public ItemDetailPage()
        {
            InitializeComponent();

            _vm = new ItemDetailViewModel();
        }

        #endregion

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = _vm.OnAppearing();
        }

        #endregion
    }
}
