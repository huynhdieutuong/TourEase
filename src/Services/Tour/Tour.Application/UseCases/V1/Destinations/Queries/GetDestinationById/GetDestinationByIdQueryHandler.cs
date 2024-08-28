using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;
using Tour.Application.Services.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.UseCases.V1.Destinations;
public class GetDestinationByIdQueryHandler : IRequestHandler<GetDestinationByIdQuery, ApiResult<DestinationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IDestinationRepository _destinationRepository;
    private readonly IDestinationService _destinationService;
    private readonly ITourCacheService _tourCacheService;

    private const string MethodName = nameof(GetDestinationByIdQueryHandler);

    public GetDestinationByIdQueryHandler(IMapper mapper,
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

    public async Task<ApiResult<DestinationDto>> Handle(GetDestinationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} Id: {request.Id}");

        var destination = await _destinationRepository.FindByIdAsync(request.Id);
        if (destination == null) throw new NotFoundException(nameof(Destination), request.Id);

        var destinations = await _tourCacheService.GetOrCreateDestinationsCacheAsync(
                async () => await _destinationRepository.FindAll().ToListAsync()
            );

        var children = _destinationService.BuildTree(destinations, destination.Id);
        destination.SubDestinations = children;

        var destinationDto = _mapper.Map<DestinationDto>(destination);

        _logger.Information($"END {MethodName} Id: {request.Id}");

        return new ApiSuccessResult<DestinationDto>(destinationDto);
    }
}
