using BuildingBlocks.Shared.ApiResult;
using Microsoft.AspNetCore.Mvc;
using TourSearch.API.Entities;
using TourSearch.API.Requests;
using TourSearch.API.Services.Interfaces;

namespace TourSearch.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TourJobsController : ControllerBase
{
    private readonly ITourJobService _tourJobService;

    public TourJobsController(ITourJobService tourJobService)
    {
        _tourJobService = tourJobService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<List<TourJob>>>> SearchTourJobs([FromQuery] SearchParams searchParams)
    {
        var result = await _tourJobService.SearchTourJobsAsync(searchParams);
        return Ok(result);
    }

    /// <summary>
    /// Get tour job by slug
    /// </summary>
    /// <param name="slug"></param>
    /// <returns></returns>
    [HttpGet("{slug}")]
    public async Task<ActionResult<ApiResult<TourJob>>> GetTourJobBySlug(string slug)
    {
        var result = await _tourJobService.GetTourJobBySlugAsync(slug);
        return Ok(result);
    }
}
