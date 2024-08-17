using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Contracts.Common.Interfaces;
public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
{
    Task<int> SaveChangesAsync();

    #region Transaction
    Task<IDbContextTransaction> BeginTransactionAsync();

    Task EndTransactionAsync();

    Task RollbackTransactionAsync();
    #endregion
}
