using BuildingBlocks.Shared.ApiResult;
using Microsoft.AspNetCore.Mvc;
using TourSearch.API.Entities;
using TourSearch.API.Services.Interfaces;

namespace TourSearch.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DestinationsController : ControllerBase
{
    private readonly IDestinationService _destinationService;

    public DestinationsController(IDestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    /// <summary>
    /// Get all destinations
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ApiResult<List<Destination>>>> GetDestinations()
    {
        var result = await _destinationService.GetDestinationsAsync();
        return Ok(result);
    }

    /// <summary>
    /// Get destination by slug
    /// </summary>
    /// <param name="slug"></param>
    /// <returns></returns>
    [HttpGet("{slug}")]
    public async Task<ActionResult<ApiResult<Destination>>> GetDestinationBySlug(string slug)
    {
        var result = await _destinationService.GetDestinationBySlugAsync(slug);
        return Ok(result);
    }
}
