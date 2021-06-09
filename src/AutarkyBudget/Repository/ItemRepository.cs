using AutarkyBudget.Models;
using AutarkyBudget.Repository.Interfaces;
using AutarkyBudget.Services.Interfaces;
using System;
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

        public Item Get(string id)
        {
            return Guid.TryParse(id, out Guid key) ? _sqliteService.Get<Item>(key) : default;
        }

        public int Add(Item item)
        {
            return _sqliteService.Insert(item, typeof(Item));
        }

        public int Update(Item item)
        {
            return _sqliteService.Update(item, typeof(Item));
        }

        public int Upsert(Item item)
        {
            return _sqliteService.Upsert(item, typeof(Item));
        }

        public bool Remove(Item item)
        {
            return _sqliteService.Delete<Item>(item.Id);
        }

        #endregion
    }
}
