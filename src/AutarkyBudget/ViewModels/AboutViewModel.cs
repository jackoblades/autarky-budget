using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        #region ICommand

        public ICommand OpenWebCommand { get; }

        #endregion

        #region Constructors

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://dwalker.xyz/"));
        }

        #endregion

        #region Methods

        public void OnAppearing()
        {
        }

        #endregion
    }
}
