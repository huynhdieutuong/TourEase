using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Contracts.Common;
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();

    #region Transaction
    Task<IDbContextTransaction> BeginTransactionAsync();

    Task EndTransactionAsync();

    Task RollbackTransactionAsync();
    #endregion
}

public interface IUnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
}