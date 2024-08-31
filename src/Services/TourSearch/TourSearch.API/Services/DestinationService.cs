using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using TourSearch.API.Entities;
using TourSearch.API.Repositories.Interfaces;
using TourSearch.API.Services.Interfaces;

namespace TourSearch.API.Services;

public class DestinationService : IDestinationService
{
    private readonly IDestinationRepository _destinationRepository;

    public DestinationService(IDestinationRepository destinationRepository)
    {
        _destinationRepository = destinationRepository;
    }

    public async Task<ApiResult<List<Destination>>> GetDestinationsAsync()
    {
        var destinations = await _destinationRepository.FindAllAsync();
        return new ApiSuccessResult<List<Destination>>(destinations);
    }

    public async Task<ApiResult<Destination>> GetDestinationBySlugAsync(string slug)
    {
        var destination = await _destinationRepository.FindSingleAsync(x => x.Slug == slug);
        if (destination == null) throw new NotFoundException(nameof(Destination), slug);
        return new ApiSuccessResult<Destination>(destination);
    }
}
