using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;

namespace Tour.Application.UseCases.V1.Destinations;
public class GetDestinationByIdQueryHandler : IRequestHandler<GetDestinationByIdQuery, ApiResult<DestinationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IDestinationRepository _destinationRepository;

    private const string MethodName = nameof(GetDestinationByIdQueryHandler);

    public GetDestinationByIdQueryHandler(IMapper mapper, ILogger logger, IDestinationRepository destinationRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _destinationRepository = destinationRepository;
    }

    public async Task<ApiResult<DestinationDto>> Handle(GetDestinationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} Id: {request.Id}");

        var destination = await _destinationRepository.FindByIdAsync(request.Id);
        var destinationDto = _mapper.Map<DestinationDto>(destination);

        _logger.Information($"END {MethodName} Id: {request.Id}");

        return new ApiSuccessResult<DestinationDto>(destinationDto);
    }
}
