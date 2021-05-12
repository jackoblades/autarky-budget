using Microcharts;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutarkyBudget.Models
{
    public class Item
    {
        #region Properties

        private static readonly Random rng = new Random();

        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Amount { get; set; }

        [Ignore]
        public decimal Value => decimal.TryParse(Amount, out decimal result) ? result : 0M;

        #endregion

        #region Constructors

        public Item()
        {
        }

        #endregion

        #region Methods

        public ChartEntry ToChartEntry()
        {
            var color = new SkiaSharp.SKColor((byte)rng.Next(256), (byte)rng.Next(256), (byte)rng.Next(256), byte.MaxValue);

            return new ChartEntry((float)Value)
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
        public static decimal SumOfValues(this IEnumerable<Item> items)
        {
            return items.ToList().Sum(x => x.Value);
        }
    }
}
