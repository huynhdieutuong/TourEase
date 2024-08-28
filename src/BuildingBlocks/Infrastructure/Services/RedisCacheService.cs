using BuildingBlocks.Contracts.Services;
using StackExchange.Redis;

namespace BuildingBlocks.Infrastructure.Services;
public class RedisCacheService : ICacheService
{
    private readonly ISerializerService _serializerService;
    private readonly IDatabase _database;

    public RedisCacheService(ISerializerService serializerService, IDatabase database)
    {
        _serializerService = serializerService;
        _database = database;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        if (value.IsNullOrEmpty) return default;

        return _serializerService.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var serializedValue = _serializerService.Serialize(value);
        await _database.StringSetAsync(key, serializedValue, expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}
