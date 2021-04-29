using AutarkyBudget.Views;
using Xamarin.Forms;

namespace AutarkyBudget
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
    }
}
