using BuildingBlocks.Infrastructure.Extensions;
using BuildingBlocks.Shared.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tour.Infrastructure.Persistence;

namespace Tour.Infrastructure;
public static class ConfigureService
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        var databaseSettings = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));
        if (databaseSettings == null || string.IsNullOrEmpty(databaseSettings.ConnectionString))
            throw new ArgumentNullException("Connection string is not configured.");

        var sqlServerRetrySettings = services.GetOptions<SqlServerRetrySettings>(nameof(SqlServerRetrySettings)) ?? throw new ArgumentNullException("SqlServerRetrySettings is not configured.");

        services.AddDbContextPool<DbContext, TourDbContext>((provider, builder) =>
        {
            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseSqlServer(
                connectionString: databaseSettings.ConnectionString,
                sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder.ExecutionStrategy(
                                dependencies => new SqlServerRetryingExecutionStrategy(
                                    dependencies: dependencies,
                                    maxRetryCount: sqlServerRetrySettings.MaxRetryCount,
                                    maxRetryDelay: sqlServerRetrySettings.MaxRetryDelay,
                                    errorNumbersToAdd: sqlServerRetrySettings.ErrorNumbersToAdd))
                            .MigrationsAssembly(typeof(TourDbContext).Assembly.GetName().Name));
        });
    }
}
