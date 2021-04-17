using Microcharts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutarkyBudget.ViewModels
{
    public class ChartViewModel : BaseViewModel
    {
        #region Properties

        public PieChart Chart { get; set; }

        #endregion

        #region Constructors

        public ChartViewModel()
        {
            // Bindings.
            Title = "Budget";
            Chart = new PieChart()
            {
                LabelTextSize = 48,
                LabelMode = LabelMode.RightOnly,
                AnimationDuration = new TimeSpan(0, 0, 1),
            };

            // Commands.
        }

        #endregion

        #region Methods

        public async Task OnAppearing()
        {
            Chart.Entries = (await DataStore.GetItemsAsync(true)).Select(x => x.ToChartEntry()).ToList();
        }

        #endregion
    }
}
