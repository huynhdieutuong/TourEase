using BuildingBlocks.Shared.Configurations;
using BuildingBlocks.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tour.Application.Interfaces;
using Tour.Infrastructure.Persistence;
using Tour.Infrastructure.Repositories;

namespace Tour.Infrastructure;
public static class ConfigureServices
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

        services.AddScoped<TourSeed>();

        services.AddScoped<ITourUnitOfWork, TourUnitOfWork>();
        services.AddScoped<ITourJobRepository, TourJobRepository>();
        services.AddScoped<IDestinationRepository, DestinationRepository>();
        services.AddScoped<ITourDetailDestinationRepository, TourDetailDestinationRepository>();
    }
}
