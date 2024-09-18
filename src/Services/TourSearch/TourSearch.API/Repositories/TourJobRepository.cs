using BuildingBlocks.Infrastructure.Common;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Paging;
using MongoDB.Bson;
using MongoDB.Driver;
using TourSearch.API.Entities;
using TourSearch.API.Persistence;
using TourSearch.API.Repositories.Interfaces;
using TourSearch.API.Requests;

namespace TourSearch.API.Repositories;

public class TourJobRepository : MongoRepositoryBase<TourJob, Guid>, ITourJobRepository
{
    public TourJobRepository(IMongoDatabase database) : base(database, CollectionNames.TourJobs)
    {
    }

    public async Task<PagedList<TourJob>> SearchTourJobsAsync(SearchParams searchParams)
    {
        var filterBuilder = Builders<TourJob>.Filter;
        var sortBuilder = Builders<TourJob>.Sort;
        var filters = new List<FilterDefinition<TourJob>>();

        // Search Term
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            var searchFilter = filterBuilder.Or(
                filterBuilder.Regex(t => t.Title, new BsonRegularExpression(searchParams.SearchTerm, "i")),
                filterBuilder.Regex(t => t.Itinerary, new BsonRegularExpression(searchParams.SearchTerm, "i"))
            );
            filters.Add(searchFilter);
        }

        // Destinations
        if (!string.IsNullOrEmpty(searchParams.DestinationIds))
        {
            var destinationIds = searchParams.DestinationIds
                .Split(',')
                .Select(idStr => Guid.TryParse(idStr, out var guid) ? guid : throw new ValidationException())
                .ToList();

            filters.Add(filterBuilder.AnyIn(t => t.DestinationIds, destinationIds));
        }

        // Duration
        if (!string.IsNullOrEmpty(searchParams.Duration))
        {
            var rangeDays = searchParams.Duration.Split('-').Select(int.Parse).ToArray();
            if (rangeDays.Length > 1)
            {
                filters.Add(filterBuilder.And(
                        filterBuilder.Gte(t => t.Days, rangeDays[0]),
                        filterBuilder.Lte(t => t.Days, rangeDays[1])
                    ));
            }
            else
            {
                filters.Add(filterBuilder.Gt(t => t.Days, rangeDays[0]));
            }
        }

        // Currency
        if (!string.IsNullOrEmpty(searchParams.Currency))
        {
            filters.Add(filterBuilder.Eq(t => t.Currency, searchParams.Currency));
        }

        // IncludeFinished
        if (!searchParams.IncludeFinished)
        {
            filters.Add(filterBuilder.Gt(t => t.ExpiredDate, DateTime.UtcNow));
        }

        var combinedFilter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;

        // Sorting
        var sort = searchParams.OrderBy switch
        {
            "ascSalary" => sortBuilder.Ascending(t => t.Salary),
            "dscSalary" => sortBuilder.Descending(t => t.Salary),
            "new" => sortBuilder.Descending(t => t.CreatedDate),
            _ => sortBuilder.Ascending(t => t.ExpiredDate)
        };

        return await PagedList<TourJob>.ToPagedList(_collection,
                                                    combinedFilter,
                                                    sort,
                                                    searchParams.PageIndex,
                                                    searchParams.PageSize);
    }

    public Task<List<TourJob>> GetTourJobsByDestinationIds(List<Guid> destinationIds)
    {
        var filter = Builders<TourJob>.Filter.AnyIn(t => t.DestinationIds, destinationIds);
        var tourJobs = _collection.Find(filter).ToListAsync();
        return tourJobs;
    }

    public async Task<int> UpdateDeletedDestinationsInTourJobs(List<TourJob> tourJobs, List<Guid> deletedDestinationIds)
    {
        var bulkOps = new List<WriteModel<TourJob>>();

        foreach (var tourJob in tourJobs)
        {
            tourJob.DestinationIds = tourJob.DestinationIds.Except(deletedDestinationIds).ToList();

            var update = Builders<TourJob>.Update.Set(t => t.DestinationIds, tourJob.DestinationIds);
            var upsertOne = new UpdateOneModel<TourJob>(
                Builders<TourJob>.Filter.Eq(t => t.Id, tourJob.Id),
                update
            );
            bulkOps.Add(upsertOne);
        }

        var result = await _collection.BulkWriteAsync(bulkOps);
        return (int)result.ModifiedCount;
    }
}
