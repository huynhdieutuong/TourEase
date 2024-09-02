using BuildingBlocks.Contracts.Common;
using BuildingBlocks.Shared.Paging;
using TourSearch.API.Entities;
using TourSearch.API.Requests;

namespace TourSearch.API.Repositories.Interfaces;

public interface ITourJobRepository : IMongoRepositoryBase<TourJob, Guid>
{
    Task<PagedList<TourJob>> SearchTourJobsAsync(SearchParams searchParams);
}
