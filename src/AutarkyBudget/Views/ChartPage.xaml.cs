using AutarkyBudget.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutarkyBudget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        #region Properties

        ChartViewModel _vm;

        #endregion

        #region Constructors

        public ChartPage()
        {
            InitializeComponent();

            BindingContext = _vm = new ChartViewModel();
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
