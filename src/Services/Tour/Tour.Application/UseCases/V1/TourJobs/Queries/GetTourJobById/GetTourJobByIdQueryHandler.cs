using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.UseCases.V1.TourJobs;
public class GetTourJobByIdQueryHandler : IRequestHandler<GetTourJobByIdQuery, ApiResult<TourJobDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourJobRepository _tourJobRepository;

    private const string MethodName = nameof(GetTourJobByIdQueryHandler);

    public GetTourJobByIdQueryHandler(IMapper mapper, ILogger logger, ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tourJobRepository = tourJobRepository;
    }

    public async Task<ApiResult<TourJobDto>> Handle(GetTourJobByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Id: {request.Id}");

        var tourJob = await _tourJobRepository.GetTourJobByIdAsync(request.Id);

        if (tourJob == null) throw new NotFoundException(nameof(TourJob), request.Id);

        var tourJobDto = _mapper.Map<TourJobDto>(tourJob);

        _logger.Information($"END {MethodName} - Id: {request.Id}");

        return new ApiSuccessResult<TourJobDto>(tourJobDto);
    }
}
