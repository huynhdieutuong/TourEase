using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
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

    private const string MethodName = nameof(CreateDestinationCommandHandler);

    public CreateDestinationCommandHandler(IMapper mapper,
                                           ILogger logger,
                                           ITourUnitOfWork tourUnitOfWork,
                                           IDestinationRepository destinationRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tourUnitOfWork = tourUnitOfWork;
        _destinationRepository = destinationRepository;
    }

    public async Task<ApiResult<DestinationDto>> Handle(CreateDestinationCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} Name: {request.Name}");

        var destination = _mapper.Map<Destination>(request);

        _destinationRepository.Add(destination);
        await _tourUnitOfWork.SaveChangesAsync();

        _logger.Information($"END {MethodName} Name: {request.Name}");

        return new ApiSuccessResult<DestinationDto>(_mapper.Map<DestinationDto>(destination));
    }
}
