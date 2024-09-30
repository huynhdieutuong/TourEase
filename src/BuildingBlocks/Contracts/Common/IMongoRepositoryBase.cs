using BuildingBlocks.Contracts.Domains;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BuildingBlocks.Contracts.Common;
public interface IMongoRepositoryBase<T, K> where T : MongoEntityBase<K>
{
    #region Query
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T> FindSingleAsync(Expression<Func<T, bool>> filter);
    Task<T> FindByIdAsync(K id);
    #endregion

    #region Command
    Task InsertAsync(T entity);
    Task InsertManyAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
    Task DeleteByIdAsync(K id);
    Task DeleteManyAsync(Expression<Func<T, bool>> filter);
    #endregion
}
