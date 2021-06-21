using Microcharts;
using NodaMoney;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutarkyBudget.Models.Domain
{
    public class BudgetItem
    {
        #region Properties

        private static readonly Random rng = new Random();

        public string Id { get; set; }

        public string Name { get; set; }

        public Money Money { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public string Amount => Money.Amount.ToString();

        #endregion

        #region Constructors

        public BudgetItem()
        {
        }

        #endregion

        #region Methods

        public ChartEntry ToChartEntry()
        {
            var color = new SkiaSharp.SKColor((byte)rng.Next(256), (byte)rng.Next(256), (byte)rng.Next(256), byte.MaxValue);

            return new ChartEntry((float)Money.Amount)
            {
                Label           = Name,
                Color           = color,
                TextColor       = color,
                ValueLabelColor = color,
            };
        }

        #endregion
    }

    public static class ItemCollectionExtensions
    {
        public static Money SumOfValues(this IEnumerable<BudgetItem> values)
        {
            IList<BudgetItem> items = values?.ToList() ?? Enumerable.Empty<BudgetItem>().ToList();
            if (!items.Any())
                return new Money();

            Money sum = new Money(0m, items[0].Money.Currency.Code);

            foreach (BudgetItem item in items)
            {
                sum = Money.Add(sum, item.Money);
            }

            return sum;
        }
    }
}
