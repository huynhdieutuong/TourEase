using BuildingBlocks.Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildingBlocks.Contracts.Common;

public interface IRepositoryBase<T, K> where T : EntityBase<K>
{
    #region Query
    IQueryable<T> FindAll(Expression<Func<T, bool>>? expression = null,
                          params Expression<Func<T, object>>[] includeProperties);

    Task<T> FindSingleAsync(Expression<Func<T, bool>> expression,
                                  params Expression<Func<T, object>>[] includeProperties);

    Task<T> FindByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    #endregion

    #region Command
    void Add(T entity);
    void AddMultiple(List<T> entities);

    void Update(T entity);
    void UpdateMultiple(List<T> entities);

    void Remove(T entity);
    void RemoveMultiple(List<T> entities);
    #endregion
}

public interface IRepositoryBase<T, K, TContext> : IRepositoryBase<T, K>
    where T : EntityBase<K>
    where TContext : DbContext
{ }
