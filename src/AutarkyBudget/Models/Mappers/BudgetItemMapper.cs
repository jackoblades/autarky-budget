using AutarkyBudget.Models.Data;
using AutarkyBudget.Models.Domain;
using NodaMoney;

namespace AutarkyBudget.Models.Mappers
{
    public static class BudgetItemMapper
    {
        public static BudgetItem ToDomainModel(this BudgetItemData itemData)
        {
            return new BudgetItem
            {
                CreationTime = itemData.CreationTime,
                Id = itemData.Id,
                Money = new Money(itemData.Amount, itemData.Currency),
                Name = itemData.Name,
            };
        }

        public static BudgetItemData ToDataModel(this BudgetItem item)
        {
            return new BudgetItemData
            {
                Amount = item.Money.Amount,
                CreationTime = item.CreationTime,
                Currency = item.Money.Currency.Code,
                Id = item.Id,
                Name = item.Name,
            };
        }
    }
}
