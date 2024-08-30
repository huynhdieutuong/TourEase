using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;

namespace Tour.Application.UseCases.V1.TourJobs;
public class GetTourJobsQueryHandler : IRequestHandler<GetTourJobsQuery, ApiResult<List<TourJobDto>>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourJobRepository _tourJobRepository;

    private const string MethodName = nameof(GetTourJobsQueryHandler);

    public GetTourJobsQueryHandler(IMapper mapper, ILogger logger, ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tourJobRepository = tourJobRepository;
    }

    public async Task<ApiResult<List<TourJobDto>>> Handle(GetTourJobsQuery request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - UserName: {request.UserName}");

        var tourJobs = await _tourJobRepository
                                .FindAll(tj => tj.CreatedBy == request.UserName,
                                         tj => tj.Detail.TourDetailDestinations)
                                .OrderByDescending(tj => tj.CreatedDate)
                                .ToListAsync();
        var tourJobsDto = _mapper.Map<List<TourJobDto>>(tourJobs);

        _logger.Information($"END {MethodName} - UserName: {request.UserName}");

        return new ApiSuccessResult<List<TourJobDto>>(tourJobsDto);
    }
}
