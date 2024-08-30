using BuildingBlocks.Shared.ApiResult;
using TourSearch.API.Entities;
using TourSearch.API.Requests;

namespace TourSearch.API.Services.Interfaces;

public interface ITourJobService
{
    Task<ApiResult<List<TourJob>>> SearchTourJobsAsync(SearchParams searchParams);
    Task<ApiResult<TourJob>> GetTourJobBySlugAsync(string slug);
}
