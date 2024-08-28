using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;
using Tour.Application.Services.Interfaces;

namespace Tour.Application.UseCases.V1.Destinations;
public class GetDestinationsQueryHandler : IRequestHandler<GetDestinationsQuery, ApiResult<List<DestinationDto>>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IDestinationRepository _destinationRepository;
    private readonly IDestinationService _destinationService;
    private readonly ITourCacheService _tourCacheService;

    private const string MethodName = nameof(GetDestinationsQueryHandler);

    public GetDestinationsQueryHandler(IMapper mapper,
                                       ILogger logger,
                                       IDestinationRepository destinationRepository,
                                       IDestinationService destinationService,
                                       ITourCacheService tourCacheService)
    {
        _mapper = mapper;
        _logger = logger;
        _destinationRepository = destinationRepository;
        _destinationService = destinationService;
        _tourCacheService = tourCacheService;
    }

    public async Task<ApiResult<List<DestinationDto>>> Handle(GetDestinationsQuery request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName}");

        var destinations = await _tourCacheService.GetOrCreateDestinationsCacheAsync(
                async () => await _destinationRepository.FindAll().ToListAsync()
            );

        var children = _destinationService.BuildTree(destinations);

        var destinationsDto = _mapper.Map<List<DestinationDto>>(children);

        _logger.Information($"END {MethodName}");

        return new ApiSuccessResult<List<DestinationDto>>(destinationsDto);
    }
}
