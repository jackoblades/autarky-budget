using AutarkyBudget.Models;
using AutarkyBudget.Repository.Interfaces;
using AutarkyBudget.Services.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AutarkyBudget.Repository
{
    public class ItemRepository : IItemRepository
    {
        #region Properties

        private readonly ISqliteService _sqliteService;

        #endregion

        #region Constructors

        public ItemRepository()
        {
            _sqliteService = DependencyService.Get<ISqliteService>();
        }

        #endregion

        #region Methods

        public IList<Item> GetAll()
        {
            return _sqliteService.ReadList<Item>();
        }

        public int Add(Item item)
        {
            return _sqliteService.Insert(item, typeof(Item));
        }

        public bool Remove(Item item)
        {
            return _sqliteService.Delete<Item>(item.Id);
        }

        #endregion
    }
}
