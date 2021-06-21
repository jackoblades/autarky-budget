using AutarkyBudget.Extensions;
using AutarkyBudget.Repository.Interfaces;
using AutarkyBudget.Services;
using Microcharts;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    public class ChartViewModel : BaseViewModel
    {
        #region Properties

        private readonly IBudgetItemRepository _itemRepository;

        public IEnumerable<string> ChartTypes { get; private set; }

        public string SelectedChart { get => _selectedChart; set => SetProperty(ref _selectedChart, value); }
        private string _selectedChart = string.Empty;

        #region Charts

        public PieChart PieChart { get; set; }

        public bool IsPieChartVisible { get => _isPieChartVisible; set => SetProperty(ref _isPieChartVisible, value); }
        private bool _isPieChartVisible = true;

        public DonutChart DonutChart { get; set; }

        public bool IsDonutChartVisible { get => _isDonutChartVisible; set => SetProperty(ref _isDonutChartVisible, value); }
        private bool _isDonutChartVisible = false;

        public RadialGaugeChart RadialGaugeChart { get; set; }

        public bool IsRadialGaugeChartVisible { get => _isRadialGaugeChartVisible; set => SetProperty(ref _isRadialGaugeChartVisible, value); }
        private bool _isRadialGaugeChartVisible = false;

        public BarChart BarChart { get; set; }

        public bool IsBarChartVisible { get => _isBarChartVisible; set => SetProperty(ref _isBarChartVisible, value); }
        private bool _isBarChartVisible = false;

        #endregion

        #endregion

        #region ICommand

        public ICommand ChartChangedCommand { get; }

        #endregion

        #region Constructors

        public ChartViewModel()
        {
            // Services.
            _itemRepository = DependencyService.Get<IBudgetItemRepository>();

            // Bindings.
            Title = "Budget";
            PieChart         = ChartBuilder.BuildPieChart();
            DonutChart       = ChartBuilder.BuildDonutChart();
            RadialGaugeChart = ChartBuilder.BuildRadialGaugeChart();
            BarChart         = ChartBuilder.BuildBarChart();
            ChartTypes = new List<string>()
            {
                nameof(PieChart),
                nameof(DonutChart),
                nameof(RadialGaugeChart),
                nameof(BarChart),
            };
            SelectedChart = ChartTypes.First();
            ExecuteChartChanged();

            // Commands.
            ChartChangedCommand = new Command(ExecuteChartChanged);
        }

        #endregion

        #region Methods

        public void OnAppearing()
        {
            IEnumerable<ChartEntry> entries = _itemRepository.GetAll().OrderBy(x => x.CreationTime).Select(x => x.ToChartEntry()).ToList();
            PieChart.Entries         = entries;
            DonutChart.Entries       = entries;
            RadialGaugeChart.Entries = entries;
            BarChart.Entries         = entries;

            Application.Current.RequestedThemeChanged += UpdateChartBackground;
        }

        public void OnDisappearing()
        {
            Application.Current.RequestedThemeChanged -= UpdateChartBackground;
        }

        private void ExecuteChartChanged()
        {
            switch (SelectedChart)
            {
                case nameof(PieChart):
                    IsPieChartVisible         = true;
                    IsDonutChartVisible       = false;
                    IsRadialGaugeChartVisible = false;
                    IsBarChartVisible         = false;
                    break;
                case nameof(DonutChart):
                    IsPieChartVisible         = false;
                    IsDonutChartVisible       = true;
                    IsRadialGaugeChartVisible = false;
                    IsBarChartVisible         = false;
                    break;
                case nameof(RadialGaugeChart):
                    IsPieChartVisible         = false;
                    IsDonutChartVisible       = false;
                    IsRadialGaugeChartVisible = true;
                    IsBarChartVisible         = false;
                    break;
                case nameof(BarChart):
                    IsPieChartVisible         = false;
                    IsDonutChartVisible       = false;
                    IsRadialGaugeChartVisible = false;
                    IsBarChartVisible         = true;
                    break;
            }
        }

        private void UpdateChartBackground(object source, AppThemeChangedEventArgs e)
        {
            PieChart.BackgroundColor         = Application.Current.GetThemeColor(AppColorTypes.Secondary);
            DonutChart.BackgroundColor       = Application.Current.GetThemeColor(AppColorTypes.Secondary);
            RadialGaugeChart.BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary);
            BarChart.BackgroundColor         = Application.Current.GetThemeColor(AppColorTypes.Secondary);
        }

        #endregion
    }
}
