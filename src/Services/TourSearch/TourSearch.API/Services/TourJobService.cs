using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using System.Linq.Expressions;
using TourSearch.API.Entities;
using TourSearch.API.Repositories.Interfaces;
using TourSearch.API.Requests;
using TourSearch.API.Services.Interfaces;

namespace TourSearch.API.Services;

public class TourJobService : ITourJobService
{
    private readonly ITourJobRepository _tourJobRepository;

    public TourJobService(ITourJobRepository tourJobRepository)
    {
        _tourJobRepository = tourJobRepository;
    }

    public async Task<ApiResult<List<TourJob>>> SearchTourJobsAsync(SearchParams searchParams)
    {
        Expression<Func<TourJob, bool>>? filter = null;

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            var comparisonType = StringComparison.CurrentCultureIgnoreCase;
            filter = x => x.Title.Contains(searchParams.SearchTerm, comparisonType)
                       || x.Itinerary.Contains(searchParams.SearchTerm, comparisonType);
        }

        var tourJobs = await _tourJobRepository.GetAllAsync(filter);
        return new ApiSuccessResult<List<TourJob>>(tourJobs);
    }

    public async Task<ApiResult<TourJob>> GetTourJobBySlugAsync(string slug)
    {
        var tourJob = await _tourJobRepository.GetBySlugAsync(slug);
        if (tourJob == null) throw new NotFoundException(nameof(TourJob), slug);
        return new ApiSuccessResult<TourJob>(tourJob);
    }
}
