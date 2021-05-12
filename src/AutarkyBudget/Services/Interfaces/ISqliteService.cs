using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutarkyBudget.Services.Interfaces
{
    public interface ISqliteService
    {
        IList<T> ReadList<T>() where T : new();
        IList<T> ReadList<T>(Expression<Func<T, bool>> predicate) where T : new();
        IList<T> ReadOrderedList<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> ordering) where T : new();
        T Get<T>(object primaryKey) where T : new();
        T ReadFirst<T>() where T : new();
        T ReadFirst<T>(Expression<Func<T, bool>> predicate) where T : new();
        int Insert(object value, Type type);
        int Update(object value, Type type);
        int Upsert(object value, Type type);
        bool UpsertList<T>(List<T> values);
        bool Delete<T>(object value);
        bool DeleteAll<T>();
        int Count<T>() where T : new();
        void Reset();
    }
}
