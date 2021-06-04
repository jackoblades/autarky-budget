using AutarkyBudget.Models;
using AutarkyBudget.ViewModels;

namespace AutarkyBudget.Views
{
    public partial class NewItemPage : PageBase
    {
        #region Properties

        private readonly NewItemViewModel _vm;

        #endregion

        #region Constructors

        public NewItemPage()
        {
            InitializeComponent();

            BindingContext = _vm = new NewItemViewModel();
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
