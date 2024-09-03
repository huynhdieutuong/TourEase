using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MassTransit;
using MediatR;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.UseCases.V1.Destinations;
public class UpdateDestinationCommandHandler : IRequestHandler<UpdateDestinationCommand, ApiResult<DestinationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourUnitOfWork _tourUnitOfWork;
    private readonly IDestinationRepository _destinationRepository;
    private readonly ITourCacheService _tourCacheService;
    private readonly IPublishEndpoint _publishEndpoint;

    private const string MethodName = nameof(UpdateDestinationCommandHandler);

    public UpdateDestinationCommandHandler(IMapper mapper,
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

    public async Task<ApiResult<DestinationDto>> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} Id: {request.Id}");

        var destination = await _destinationRepository.FindByIdAsync(request.Id);
        if (destination == null) throw new NotFoundException(nameof(Destination), request.Id);

        _mapper.Map(request, destination);
        _destinationRepository.Update(destination);

        var destinationDto = _mapper.Map<DestinationDto>(destination);
        await _publishEndpoint.Publish(_mapper.Map<DestinationUpdated>(destinationDto));

        await _tourUnitOfWork.SaveChangesAsync();

        await _tourCacheService.InvalidDestinationsCacheAsync();

        _logger.Information($"END {MethodName} Id: {request.Id}");

        return new ApiSuccessResult<DestinationDto>(destinationDto);
    }
}
