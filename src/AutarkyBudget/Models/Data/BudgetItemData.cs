using SQLite;
using System;

namespace AutarkyBudget.Models.Data
{
    public class BudgetItemData
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTimeOffset CreationTime { get; set; }
    }
}
