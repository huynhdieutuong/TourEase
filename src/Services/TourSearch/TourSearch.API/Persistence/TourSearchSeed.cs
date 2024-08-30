using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Shared.Configurations;
using MongoDB.Bson;
using MongoDB.Driver;
using TourSearch.API.Entities;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Persistence;

public class TourSearchSeed
{
    private readonly ISerializerService _serializerService;
    private readonly ILogger _logger;
    private readonly IMongoDatabase Database;

    public TourSearchSeed(ISerializerService serializerService,
                          ILogger logger,
                          IMongoClient client,
                          MongoDbSettings settings)
    {
        _serializerService = serializerService;
        _logger = logger;
        Database = client.GetDatabase(settings.DatabaseName);
    }

    public async Task SeedDataAsync()
    {
        try
        {
            var tourJobCollection = Database.GetCollection<TourJob>(CollectionNames.TourJobs);
            var destinationCollection = Database.GetCollection<Destination>(CollectionNames.Destinations);

            // Create indexes
            await EnsureIndexesAsync(tourJobCollection, destinationCollection);

            // Seed data
            if (await destinationCollection.EstimatedDocumentCountAsync() == 0)
            {
                _logger.Information("No destinations data - will attempt to seed");
                var destinationsJson = await File.ReadAllTextAsync("Persistence/destinations.json");
                var destinations = _serializerService.Deserialize<List<Destination>>(destinationsJson);

                await destinationCollection.InsertManyAsync(destinations);
            }

            if (await tourJobCollection.EstimatedDocumentCountAsync() == 0)
            {
                _logger.Information("No tour jobs data - will attempt to seed");
                var tourJobsJson = await File.ReadAllTextAsync("Persistence/tourJobs.json");
                var tourJobs = _serializerService.Deserialize<List<TourJob>>(tourJobsJson);

                await tourJobCollection.InsertManyAsync(tourJobs);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while seeding the TourSearch database.");
            throw;
        }
    }

    private async Task EnsureIndexesAsync(IMongoCollection<TourJob> tourJobCollection,
                                          IMongoCollection<Destination> destinationCollection)
    {
        // Fetch the list of indexes
        var tourJobIndexes = await tourJobCollection.Indexes.ListAsync();
        var destinationIndexes = await destinationCollection.Indexes.ListAsync();

        var tourJobIndexNames = await tourJobIndexes.ToListAsync();
        var destinationIndexNames = await destinationIndexes.ToListAsync();

        // Create index on Slug for TourJob collection
        if (!IndexExists(tourJobIndexNames, "slug_1"))
        {
            var tourJobSlugIndex = Builders<TourJob>.IndexKeys.Ascending(t => t.Slug);
            var tourJobSlugIndexModel = new CreateIndexModel<TourJob>(tourJobSlugIndex, new CreateIndexOptions { Unique = true });
            await tourJobCollection.Indexes.CreateOneAsync(tourJobSlugIndexModel);
            _logger.Information("Created unique index on Slug for TourJob collection.");
        }

        // Create index on Title for TourJob collection
        if (!IndexExists(tourJobIndexNames, "title_1"))
        {
            var tourJobTitleIndex = Builders<TourJob>.IndexKeys.Ascending(t => t.Title);
            var tourJobTitleIndexModel = new CreateIndexModel<TourJob>(tourJobTitleIndex);
            await tourJobCollection.Indexes.CreateOneAsync(tourJobTitleIndexModel);
            _logger.Information("Created index on Title for TourJob collection.");
        }

        // Create index on Slug for Destination collection if it does not exist
        if (!IndexExists(destinationIndexNames, "slug_1"))
        {
            var destinationSlugIndex = Builders<Destination>.IndexKeys.Ascending(d => d.Slug);
            var destinationSlugIndexModel = new CreateIndexModel<Destination>(destinationSlugIndex, new CreateIndexOptions { Unique = true });
            await destinationCollection.Indexes.CreateOneAsync(destinationSlugIndexModel);
            _logger.Information("Created unique index on Slug for Destination collection.");
        }
    }

    private bool IndexExists(IEnumerable<BsonDocument> indexList, string indexName)
    {
        return indexList.Any(idx => idx["name"].AsString == indexName);
    }
}
