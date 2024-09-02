using BuildingBlocks.Infrastructure.Common;
using MongoDB.Driver;
using TourSearch.API.Entities;
using TourSearch.API.Persistence;
using TourSearch.API.Repositories.Interfaces;

namespace TourSearch.API.Repositories;

public class DestinationRepository : MongoRepositoryBase<Destination, Guid>, IDestinationRepository
{
    public DestinationRepository(IMongoDatabase database) : base(database, CollectionNames.Destinations)
    {
    }
}
