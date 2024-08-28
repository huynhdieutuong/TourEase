using BuildingBlocks.Contracts.Services;
using Tour.Domain.Entities;

namespace Tour.Application.Interfaces;
public interface ITourCacheService : ICacheService
{
    Task<List<Destination>> GetOrCreateDestinationsCacheAsync(Func<Task<List<Destination>>> func);
    Task InvalidDestinationsCacheAsync();
}
