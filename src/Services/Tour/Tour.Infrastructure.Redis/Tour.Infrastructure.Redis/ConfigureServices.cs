using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Infrastructure.Services;
using BuildingBlocks.Shared.Configurations;
using BuildingBlocks.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Tour.Application.Interfaces;
using Tour.Infrastructure.Redis.Services;

namespace Tour.Infrastructure.Redis;

public static class ConfigureServices
{
    public static void AddInfrastructureRedisServices(this IServiceCollection services)
    {
        var cacheSettings = services.GetOptions<CacheSettings>(nameof(CacheSettings));
        if (cacheSettings == null || string.IsNullOrEmpty(cacheSettings.ConnectionString))
            throw new ArgumentNullException("Cache connection string is not configured.");

        if (!cacheSettings.Enable) return;
        var connectionMultiplexer = ConnectionMultiplexer.Connect(cacheSettings.ConnectionString);

        services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);
        services.AddSingleton(connectionMultiplexer.GetDatabase());

        services.AddScoped<ISerializerService, SerializerService>();
        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddScoped<ITourCacheService, TourRedisCacheService>();
    }
}
