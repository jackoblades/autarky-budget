using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutarkyBudget.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwipeItemListTemplate : ContentView
    {
        #region Bindings

        public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(nameof(DeleteCommand), typeof(ICommand), typeof(SwipeItemListTemplate), null, BindingMode.TwoWay);
        public ICommand DeleteCommand
        {
            get { return GetValue(DeleteCommandProperty) as ICommand; }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public static readonly BindableProperty EditCommandProperty = BindableProperty.Create(nameof(EditCommand), typeof(ICommand), typeof(SwipeItemListTemplate), null, BindingMode.TwoWay);
        public ICommand EditCommand
        {
            get { return GetValue(EditCommandProperty) as ICommand; }
            set { SetValue(EditCommandProperty, value); }
        }

        #endregion

        #region Constructors

        public SwipeItemListTemplate()
        {
            InitializeComponent();
        }

        #endregion
    }
}
