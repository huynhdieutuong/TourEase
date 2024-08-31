using BuildingBlocks.Infrastructure.Common;
using BuildingBlocks.Shared.Paging;
using MongoDB.Bson;
using MongoDB.Driver;
using TourSearch.API.Entities;
using TourSearch.API.Persistence;
using TourSearch.API.Repositories.Interfaces;
using TourSearch.API.Requests;

namespace TourSearch.API.Repositories;

public class TourJobRepository : MongoRepositoryBase<TourJob>, ITourJobRepository
{
    public TourJobRepository(IMongoDatabase database) : base(database, CollectionNames.TourJobs)
    {
    }

    public async Task<PagedList<TourJob>> SearchTourJobsAsync(SearchParams searchParams)
    {
        var filterBuilder = Builders<TourJob>.Filter;
        var sortBuilder = Builders<TourJob>.Sort;

        var filters = new List<FilterDefinition<TourJob>>();

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            var searchFilter = filterBuilder.Or(
                filterBuilder.Regex(t => t.Title, new BsonRegularExpression(searchParams.SearchTerm, "i")),
                filterBuilder.Regex(t => t.Itinerary, new BsonRegularExpression(searchParams.SearchTerm, "i"))
            );
            filters.Add(searchFilter);
        }

        switch (searchParams.Filter?.ToLower())
        {
            case "finished":
                filters.Add(filterBuilder.Lt(t => t.ExpiredDate, DateTimeOffset.UtcNow));
                break;
            case "endingsoon":
                filters.Add(filterBuilder.And(
                    filterBuilder.Lt(t => t.ExpiredDate, DateTimeOffset.UtcNow.AddHours(6)),
                    filterBuilder.Gt(t => t.ExpiredDate, DateTimeOffset.UtcNow)
                ));
                break;
            default:
                filters.Add(filterBuilder.Gt(t => t.ExpiredDate, DateTimeOffset.UtcNow));
                break;
        }

        var combinedFilter = filterBuilder.And(filters);

        var sort = searchParams.OrderBy?.ToLower() switch
        {
            "day" => sortBuilder.Ascending(t => t.Days),
            "salary" => sortBuilder.Descending(t => t.Salary),
            "new" => sortBuilder.Descending(t => t.CreatedDate),
            _ => sortBuilder.Ascending(t => t.ExpiredDate)
        };

        return await PagedList<TourJob>.ToPagedList(_collection,
                                                    combinedFilter,
                                                    sort,
                                                    searchParams.PageIndex,
                                                    searchParams.PageSize);
    }
}
