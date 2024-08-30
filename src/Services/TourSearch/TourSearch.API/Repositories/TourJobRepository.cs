using BuildingBlocks.Infrastructure.Common;
using MongoDB.Driver;
using TourSearch.API.Entities;
using TourSearch.API.Persistence;
using TourSearch.API.Repositories.Interfaces;

namespace TourSearch.API.Repositories;

public class TourJobRepository : MongoRepositoryBase<TourJob>, ITourJobRepository
{
    public TourJobRepository(IMongoDatabase database) : base(database, CollectionNames.TourJobs)
    {
    }
}
