using AutarkyBudget.Models;
using AutarkyBudget.ViewModels;
using Xamarin.Forms;

namespace AutarkyBudget.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}
