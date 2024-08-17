using BuildingBlocks.Contracts.Common.Interfaces;
using BuildingBlocks.Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildingBlocks.Infrastructure.Common;
public class RepositoryBase<T, K, TContext> : IRepositoryBase<T, K, TContext>
    where T : EntityBase<K>
    where TContext : DbContext
{
    private readonly TContext _context;

    public RepositoryBase(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    #region Query
    public IQueryable<T> FindAll(Expression<Func<T, bool>>? expression = null,
                                 params Expression<Func<T, object>>[] includeProperties)
    {
        var items = _context.Set<T>().AsNoTracking();

        if (includeProperties != null)
        {
            items = includeProperties.Aggregate(items,
                (current, includeProperty) => current.Include(includeProperty));
        }

        if (expression != null)
        {
            items = items.Where(expression);
        }

        return items;
    }

    public async Task<T> FindSingleAsync(Expression<Func<T, bool>> expression,
                                         params Expression<Func<T, object>>[] includeProperties)
        => await FindAll(expression, includeProperties).SingleOrDefaultAsync();

    public async Task<T?> FindByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
        => await FindAll(x => x.Id.Equals(id), includeProperties).SingleOrDefaultAsync();
    #endregion

    #region Command
    public void Add(T entity)
        => _context.Set<T>().Add(entity);

    public void AddMultiple(List<T> entities)
        => _context.Set<T>().AddRange(entities);

    public void Remove(T entity)
        => _context.Set<T>().Remove(entity);

    public void RemoveMultiple(List<T> entities)
        => _context.Set<T>().RemoveRange(entities);

    public void Update(T entity)
        => _context.Set<T>().Update(entity);

    public void UpdateMultiple(List<T> entities)
        => _context.Set<T>().UpdateRange(entities);
    #endregion
}
