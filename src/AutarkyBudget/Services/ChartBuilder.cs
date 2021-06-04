using AutarkyBudget.Extensions;
using Microcharts;
using System;
using Xamarin.Forms;

namespace AutarkyBudget.Services
{
    public static class ChartBuilder
    {
        #region Methods

        public static PieChart BuildPieChart()
        {
            return new PieChart()
            {
                AnimationDuration = new TimeSpan(0, 0, 1),
                BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary),
                LabelMode = LabelMode.RightOnly,
                LabelTextSize = 48f,
            };
        }

        public static DonutChart BuildDonutChart()
        {
            return new DonutChart()
            {
                AnimationDuration = new TimeSpan(0, 0, 1),
                BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary),
                LabelMode = LabelMode.RightOnly,
                LabelTextSize = 48f,
            };
        }

        public static RadialGaugeChart BuildRadialGaugeChart()
        {
            return new RadialGaugeChart()
            {
                AnimationDuration = new TimeSpan(0, 0, 1),
                BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary),
                LabelTextSize = 48f,
            };
        }

        public static BarChart BuildBarChart()
        {
            return new BarChart()
            {
                AnimationDuration = new TimeSpan(0, 0, 1),
                BackgroundColor = Application.Current.GetThemeColor(AppColorTypes.Secondary),
            };
        }

        #endregion
    }
}
