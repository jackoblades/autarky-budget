using AutarkyBudget.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutarkyBudget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        #region Properties

        ItemsViewModel _vm;

        #endregion

        #region Constructors

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _vm = new ItemsViewModel();
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
