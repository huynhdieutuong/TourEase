using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Paging;
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
        var pagedTourJobs = await _tourJobRepository.SearchTourJobsAsync(searchParams);

        return new ApiSuccessResult<List<TourJob>, MetaData>(pagedTourJobs,
                                                             pagedTourJobs.GetMetaData());
    }

    public async Task<ApiResult<TourJob>> GetTourJobBySlugAsync(string slug)
    {
        var tourJob = await _tourJobRepository.FindSingleAsync(x => x.Slug == slug);
        if (tourJob == null) throw new NotFoundException(nameof(TourJob), slug);
        return new ApiSuccessResult<TourJob>(tourJob);
    }
}
