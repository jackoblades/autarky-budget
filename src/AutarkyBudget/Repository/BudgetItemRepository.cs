using AutarkyBudget.Models.Data;
using AutarkyBudget.Models.Domain;
using AutarkyBudget.Models.Mappers;
using AutarkyBudget.Repository.Interfaces;
using AutarkyBudget.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AutarkyBudget.Repository
{
    public class BudgetItemRepository : IBudgetItemRepository
    {
        #region Properties

        private readonly ISqliteService _sqliteService;

        #endregion

        #region Constructors

        public BudgetItemRepository()
        {
            _sqliteService = DependencyService.Get<ISqliteService>();
        }

        #endregion

        #region Methods

        public IList<BudgetItem> GetAll()
        {
            return _sqliteService.ReadList<BudgetItemData>().Select(x => x.ToDomainModel()).ToList();
        }

        public BudgetItem Get(string id)
        {
            return Guid.TryParse(id, out Guid key) ? _sqliteService.Get<BudgetItemData>(key).ToDomainModel() : default;
        }

        public int Add(BudgetItem item)
        {
            return _sqliteService.Insert(item.ToDataModel(), typeof(BudgetItemData));
        }

        public int Update(BudgetItem item)
        {
            return _sqliteService.Update(item.ToDataModel(), typeof(BudgetItemData));
        }

        public int Upsert(BudgetItem item)
        {
            return _sqliteService.Upsert(item.ToDataModel(), typeof(BudgetItemData));
        }

        public bool Remove(BudgetItem item)
        {
            return _sqliteService.Delete<BudgetItemData>(item.Id);
        }

        #endregion
    }
}
