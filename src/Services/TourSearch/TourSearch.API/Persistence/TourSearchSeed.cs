using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Shared.Configurations;
using MongoDB.Bson;
using MongoDB.Driver;
using TourSearch.API.Entities;
using TourSearch.API.HttpClients.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Persistence;

public class TourSearchSeed
{
    private readonly ISerializerService _serializerService;
    private readonly ILogger _logger;
    private readonly IMongoDatabase Database;
    private readonly ITourHttpClient _tourHttpClient;

    public TourSearchSeed(ISerializerService serializerService,
                          ILogger logger,
                          IMongoClient client,
                          MongoDbSettings settings,
                          ITourHttpClient tourHttpClient)
    {
        _serializerService = serializerService;
        _logger = logger;
        Database = client.GetDatabase(settings.DatabaseName);
        _tourHttpClient = tourHttpClient;
    }

    public async Task SeedDataAsync()
    {
        while (true) // Retry until SeedData successful
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
                    //var destinationsJson = await File.ReadAllTextAsync("Persistence/destinations.json");
                    //var destinations = _serializerService.Deserialize<List<Destination>>(destinationsJson);
                    var destinations = await _tourHttpClient.GetDestinationsFromTourService();
                    await destinationCollection.InsertManyAsync(destinations);
                }

                if (await tourJobCollection.EstimatedDocumentCountAsync() == 0)
                {
                    _logger.Information("No tour jobs data - will attempt to seed");
                    //var tourJobsJson = await File.ReadAllTextAsync("Persistence/tourJobs.json");
                    //var tourJobs = _serializerService.Deserialize<List<TourJob>>(tourJobsJson);
                    var tourJobs = await _tourHttpClient.GetTourJobsFromTourService();
                    await tourJobCollection.InsertManyAsync(tourJobs);
                }

                break;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while seeding the TourSearch database. Retrying...");
                await Task.Delay(5000);
            }
        }
    }

    private async Task EnsureIndexesAsync(IMongoCollection<TourJob> tourJobCollection,
                                          IMongoCollection<Destination> destinationCollection)
    {
        var tourJobIndexes = await tourJobCollection.Indexes.ListAsync();
        var destinationIndexes = await destinationCollection.Indexes.ListAsync();

        var tourJobIndexNames = await tourJobIndexes.ToListAsync();
        var destinationIndexNames = await destinationIndexes.ToListAsync();

        // TourJob
        if (!IndexExists(tourJobIndexNames, "slug_1"))
        {
            var tourJobSlugIndex = Builders<TourJob>.IndexKeys.Ascending(t => t.Slug);
            var tourJobSlugIndexModel = new CreateIndexModel<TourJob>(tourJobSlugIndex, new CreateIndexOptions { Unique = true });
            await tourJobCollection.Indexes.CreateOneAsync(tourJobSlugIndexModel);
            _logger.Information("Created unique index on Slug for TourJob collection.");
        }

        if (!IndexExists(tourJobIndexNames, "text_search_title_itinerary"))
        {
            var textIndex = Builders<TourJob>.IndexKeys
                .Text(t => t.Title)
                .Text(t => t.Itinerary);

            var indexModel = new CreateIndexModel<TourJob>(textIndex);
            await tourJobCollection.Indexes.CreateOneAsync(indexModel);
            _logger.Information("Created text index on Title and Itinerary.");
        }

        if (!IndexExists(tourJobIndexNames, "expireddate_salary_createddate"))
        {
            var compoundIndex = Builders<TourJob>.IndexKeys
                .Ascending(t => t.ExpiredDate)
                .Ascending(t => t.Salary)
                .Descending(t => t.CreatedDate);

            var indexModel = new CreateIndexModel<TourJob>(compoundIndex);
            await tourJobCollection.Indexes.CreateOneAsync(indexModel);
            _logger.Information("Created compound index on ExpiredDate, Salary, and CreatedDate.");
        }

        if (!IndexExists(tourJobIndexNames, "destinationids_1"))
        {
            var destinationIdsIndex = Builders<TourJob>.IndexKeys
                .Ascending(t => t.DestinationIds);

            var indexModel = new CreateIndexModel<TourJob>(destinationIdsIndex);
            await tourJobCollection.Indexes.CreateOneAsync(indexModel);
            _logger.Information("Created index on DestinationIds.");
        }

        // Destination
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
