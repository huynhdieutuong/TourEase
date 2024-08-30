using BuildingBlocks.Shared.ApiResult;
using TourSearch.API.Entities;

namespace TourSearch.API.Services.Interfaces;

public interface IDestinationService
{
    Task<ApiResult<List<Destination>>> GetDestinationsAsync();
    Task<ApiResult<Destination>> GetDestinationBySlugAsync(string slug);
}
