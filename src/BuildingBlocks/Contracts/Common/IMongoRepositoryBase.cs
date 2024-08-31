using BuildingBlocks.Contracts.Domains;
using System.Linq.Expressions;

namespace BuildingBlocks.Contracts.Common;
public interface IMongoRepositoryBase<T> where T : MongoEntityBase
{
    #region Query
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T> FindSingleAsync(Expression<Func<T, bool>> filter);
    Task<T> FindByIdAsync(Guid id);
    #endregion

    #region Command
    Task InsertAsync(T entity);
    Task InsertManyAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteByIdAsync(Guid id);
    Task DeleteManyAsync(Expression<Func<T, bool>> filter);
    #endregion
}
