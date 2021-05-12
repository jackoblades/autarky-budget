using AutarkyBudget.Models;
using System.Collections.Generic;

namespace AutarkyBudget.Repository.Interfaces
{
    interface IItemRepository
    {
        IList<Item> GetAll();
        int Add(Item item);
        bool Remove(Item item);
    }
}
