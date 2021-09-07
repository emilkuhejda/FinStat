using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinStat.DataAccess.Entities;
using SQLite;

namespace FinStat.DataAccess
{
    public interface IAppDbContext
    {
        AsyncTableQuery<InternalValueEntity> InternalValues { get; }

        Task RunInTransactionAsync(Action<SQLiteConnection> action);

        Task<int> GetVersionNumberAsync();

        Task UpdateVersionNumberAsync(int versionNumber);

        Task CreateTablesAsync(params Type[] types);

        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : new();

        Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : new();

        Task<IEnumerable<T>> GetAllWithChildrenAsync<T>(Expression<Func<T, bool>> predicate) where T : new();

        Task<T> GetWithChildrenAsync<T>(object primaryKey) where T : new();

        Task InsertAllAsync<T>(IEnumerable<T> items);

        Task InsertAsync<T>(T item);

        Task UpdateAsync<T>(T item);

        Task UpdateAllAsync(IEnumerable items);

        Task DeleteAllIdsAsync<T>(IEnumerable<object> primaryKey);

        Task DeleteAsync<T>(object primaryKey) where T : new();

        Task DeleteWithChildrenAsync<T>(object primaryKey) where T : new();

        Task DeleteAllAsync<T>() where T : new();

        Task DropTable(Type type);

        Task InsertOrReplaceAsync(object obj);

        Task CloseAsync();
    }
}
