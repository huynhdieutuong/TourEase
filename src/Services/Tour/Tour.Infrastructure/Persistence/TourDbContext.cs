﻿using BuildingBlocks.Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Tour.Domain.Entities;

namespace Tour.Infrastructure.Persistence;
public class TourDbContext : DbContext
{
    public TourDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TourJob> TourJob { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modified = ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Modified
                                 || e.State == EntityState.Added
                                 || e.State == EntityState.Deleted);

        foreach (var item in modified)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    if (item.Entity is IDateTracking addedDateEntity)
                    {
                        addedDateEntity.CreatedDate = DateTimeOffset.UtcNow;
                    }
                    if (item.Entity is IUserTracking addedUserEntity)
                    {
                        addedUserEntity.CreatedBy = "test";
                    }
                    item.State = EntityState.Added;
                    break;
                case EntityState.Modified:
                    Entry(item.Entity).Property("Id").IsModified = false;
                    if (item.Entity is IDateTracking modifiedDateEntity)
                    {
                        modifiedDateEntity.UpdatedDate = DateTimeOffset.UtcNow;
                    }
                    if (item.Entity is IUserTracking modifiedUserEntity)
                    {
                        modifiedUserEntity.UpdatedBy = "test1";
                    }
                    item.State = EntityState.Modified;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
