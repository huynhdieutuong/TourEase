using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Infrastructure.Services;
using BuildingBlocks.Logging;
using BuildingBlocks.Shared.Configurations;
using BuildingBlocks.Shared.Extensions;
using MongoDB.Driver;
using TourSearch.API.HttpClients;
using TourSearch.API.HttpClients.Interfaces;
using TourSearch.API.Persistence;
using TourSearch.API.Repositories;
using TourSearch.API.Repositories.Interfaces;
using TourSearch.API.Services;
using TourSearch.API.Services.Interfaces;

namespace TourSearch.API.Extensions;

public static class ServiceExtensions
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

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISerializerService, SerializerService>();
        services.AddTransient<LoggingDelegatingHandler>();

        services.AddScoped<ITourJobService, TourJobService>();
        services.AddScoped<IDestinationService, DestinationService>();
    }

    public static void ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureTourHttpClients(services, configuration);
    }

    private static void ConfigureTourHttpClients(IServiceCollection services, IConfiguration configuration)
    {
        var baseAddress = configuration["ServiceUrls:TourUrl"];

        services.AddHttpClient<ITourHttpClient, TourHttpClient>("TourAPI", (sp, cl) =>
        {
            cl.BaseAddress = new Uri($"{baseAddress}/api/");
        }).AddHttpMessageHandler<LoggingDelegatingHandler>()
            .UseExponentialHttpRetryPolicy()
            .UseCircuitBreakerPolicy()
            .ConfigureTimeoutPolicy();
    }
}
