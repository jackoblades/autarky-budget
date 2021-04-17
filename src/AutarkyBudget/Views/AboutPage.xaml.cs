using AutarkyBudget.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutarkyBudget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        #region Properties

        AboutViewModel _vm;

        #endregion

        #region Constructors

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = _vm = new AboutViewModel();
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
