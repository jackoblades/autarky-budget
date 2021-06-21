using AutarkyBudget.Models.Domain;
using System.Collections.Generic;

namespace AutarkyBudget.Repository.Interfaces
{
    interface IBudgetItemRepository
    {
        IList<BudgetItem> GetAll();
        BudgetItem Get(string id);
        int Add(BudgetItem item);
        int Update(BudgetItem item);
        int Upsert(BudgetItem item);
        bool Remove(BudgetItem item);
    }
}
