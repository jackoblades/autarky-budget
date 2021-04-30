using AutarkyBudget.Services;
using Xamarin.Forms;

namespace AutarkyBudget
{
    public partial class App : Application
    {
        #region Constructors

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        #endregion

        #region Event Handlers

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion
    }
}
