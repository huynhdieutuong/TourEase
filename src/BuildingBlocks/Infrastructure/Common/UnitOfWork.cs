using BuildingBlocks.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Infrastructure.Common;
public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly TContext _context;

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public void Dispose() => _context.Dispose();

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

    #region Transaction
    public async Task<IDbContextTransaction> BeginTransactionAsync()
        => await _context.Database.BeginTransactionAsync();

    public async Task EndTransactionAsync()
    {
        await SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
        => await _context.Database.RollbackTransactionAsync();
    #endregion
}
