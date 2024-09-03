using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using BuildingBlocks.Shared.ApiResult;
using MassTransit;
using MediatR;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.UseCases.V1.Destinations;
public class CreateDestinationCommandHandler : IRequestHandler<CreateDestinationCommand, ApiResult<DestinationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourUnitOfWork _tourUnitOfWork;
    private readonly IDestinationRepository _destinationRepository;
    private readonly ITourCacheService _tourCacheService;
    private readonly IPublishEndpoint _publishEndpoint;

    private const string MethodName = nameof(CreateDestinationCommandHandler);

    public CreateDestinationCommandHandler(IMapper mapper,
                                           ILogger logger,
                                           ITourUnitOfWork tourUnitOfWork,
                                           IDestinationRepository destinationRepository,
                                           ITourCacheService tourCacheService,
                                           IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _logger = logger;
        _tourUnitOfWork = tourUnitOfWork;
        _destinationRepository = destinationRepository;
        _tourCacheService = tourCacheService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ApiResult<DestinationDto>> Handle(CreateDestinationCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} Name: {request.Name}");

        var destination = _mapper.Map<Destination>(request);

        _destinationRepository.Add(destination);

        var destinationDto = _mapper.Map<DestinationDto>(destination);
        await _publishEndpoint.Publish(_mapper.Map<DestinationCreated>(destinationDto));

        await _tourUnitOfWork.SaveChangesAsync();

        await _tourCacheService.InvalidDestinationsCacheAsync();

        _logger.Information($"END {MethodName} Name: {request.Name}");

        return new ApiSuccessResult<DestinationDto>(destinationDto);
    }
}
