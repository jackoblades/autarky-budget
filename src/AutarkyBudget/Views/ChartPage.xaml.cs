using AutarkyBudget.ViewModels;
using Xamarin.Forms.Xaml;

namespace AutarkyBudget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : PageBase
    {
        #region Properties

        private readonly ChartViewModel _vm;

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
            _vm.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _vm.OnDisappearing();
            base.OnDisappearing();
        }

        #endregion
    }
}
