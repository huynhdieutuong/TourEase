using BuildingBlocks.Shared.Configurations;
using BuildingBlocks.Shared.Extensions;
using MongoDB.Driver;
using TourSearch.API.Persistence;
using TourSearch.API.Repositories;
using TourSearch.API.Repositories.Interfaces;

namespace TourSearch.API.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        var databaseSettings = services.GetOptions<MongoDbSettings>(nameof(MongoDbSettings));
        if (databaseSettings == null || string.IsNullOrEmpty(databaseSettings.ConnectionString))
            throw new ArgumentNullException("Tour Search connection string is not configured.");
        services.AddSingleton(databaseSettings);

        var mongoDbConnectionString = $"{databaseSettings.ConnectionString}/{databaseSettings.DatabaseName}?authSource=admin";
        services.AddSingleton<IMongoClient>(new MongoClient(mongoDbConnectionString));

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseSettings.DatabaseName);
        });

        services.AddScoped<TourSearchSeed>();

        services.AddScoped<ITourJobRepository, TourJobRepository>();
        services.AddScoped<IDestinationRepository, DestinationRepository>();
    }
}
