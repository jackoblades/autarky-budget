using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutarkyBudget.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBase : ContentPage
    {
        public PageBase()
        {
            InitializeComponent();
        }
    }
}
