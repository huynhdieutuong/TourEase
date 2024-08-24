using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
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

    private const string MethodName = nameof(UpdateDestinationCommandHandler);

    public UpdateDestinationCommandHandler(IMapper mapper,
                                           ILogger logger,
                                           ITourUnitOfWork tourUnitOfWork,
                                           IDestinationRepository destinationRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tourUnitOfWork = tourUnitOfWork;
        _destinationRepository = destinationRepository;
    }

    public async Task<ApiResult<DestinationDto>> Handle(UpdateDestinationCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} Id: {request.Id}");

        var destination = await _destinationRepository.FindByIdAsync(request.Id);
        if (destination == null) throw new NotFoundException(nameof(Destination), request.Id);

        _mapper.Map(request, destination);
        _destinationRepository.Update(destination);

        await _tourUnitOfWork.SaveChangesAsync();

        _logger.Information($"END {MethodName} Id: {request.Id}");

        return new ApiSuccessResult<DestinationDto>(_mapper.Map<DestinationDto>(destination));
    }
}
