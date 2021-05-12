using AutarkyBudget.Repository;
using Xamarin.Forms;

namespace AutarkyBudget
{
    public partial class App : Application
    {
        #region Constructors

        public App()
        {
            InitializeComponent();
            RegisterServices();
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

        #region Methods

        private void RegisterServices()
        {
            DependencyService.Register<ItemRepository>();
        }

        #endregion
    }
}
