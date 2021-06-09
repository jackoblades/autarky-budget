using AutarkyBudget.Models;
using System.Collections.Generic;

namespace AutarkyBudget.Repository.Interfaces
{
    interface IItemRepository
    {
        IList<Item> GetAll();
        Item Get(string id);
        int Add(Item item);
        int Update(Item item);
        int Upsert(Item item);
        bool Remove(Item item);
    }
}
