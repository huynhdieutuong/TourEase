using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Infrastructure.Services;
using StackExchange.Redis;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Infrastructure.Redis.Services;
public class TourRedisCacheService : RedisCacheService, ITourCacheService
{
    private const string _destinationsKey = "Destinations";

    public TourRedisCacheService(ISerializerService serializerService, IDatabase database) : base(serializerService, database)
    {
    }

    public async Task<List<Destination>> GetOrCreateDestinationsCacheAsync(Func<Task<List<Destination>>> func)
    {
        var destinationsCacheValue = await GetAsync<List<Destination>>(_destinationsKey);
        if (destinationsCacheValue != null) return destinationsCacheValue;

        var result = await func();
        await SetAsync(_destinationsKey, result);

        return result;
    }

    public async Task InvalidDestinationsCacheAsync()
    {
        await RemoveAsync(_destinationsKey);
    }
}
