using AutarkyBudget.Extensions;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AutarkyBudget.ViewModels
{
    public class ChartViewModel : BaseViewModel
    {
        #region Properties

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
            // Bindings.
            Title = "Budget";
            PieChart = new PieChart()
            {
                AnimationDuration = new TimeSpan(0, 0, 1),
                BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary),
                LabelMode = LabelMode.RightOnly,
                LabelTextSize = 48f,
            };
            DonutChart = new DonutChart()
            {
                AnimationDuration = new TimeSpan(0, 0, 1),
                BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary),
                LabelMode = LabelMode.RightOnly,
                LabelTextSize = 48f,
            };
            RadialGaugeChart = new RadialGaugeChart()
            {
                AnimationDuration = new TimeSpan(0, 0, 1),
                BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary),
                LabelTextSize = 48f,
            };
            BarChart = new BarChart()
            {
                AnimationDuration = new TimeSpan(0, 0, 1),
                BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary),
            };
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

        public async Task OnAppearing()
        {
            IEnumerable<ChartEntry> entries = (await DataStore.GetItemsAsync(true)).Select(x => x.ToChartEntry()).ToList();
            PieChart.Entries         = entries;
            DonutChart.Entries       = entries;
            RadialGaugeChart.Entries = entries;
            BarChart.Entries         = entries;
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

        #endregion
    }
}
