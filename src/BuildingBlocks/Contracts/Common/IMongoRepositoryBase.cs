using BuildingBlocks.Contracts.Domains;
using System.Linq.Expressions;

namespace BuildingBlocks.Contracts.Common;
public interface IMongoRepositoryBase<T> where T : MongoEntityBase
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetBySlugAsync(string slug);
    Task InsertAsync(T entity);
    Task InsertManyAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteByIdAsync(Guid id);
    Task DeleteManyAsync(Expression<Func<T, bool>> filter);
}
