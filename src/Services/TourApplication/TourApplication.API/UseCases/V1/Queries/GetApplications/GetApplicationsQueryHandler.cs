using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using MediatR;
using TourApplication.API.DTOs;
using TourApplication.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, ApiResult<List<ApplicationDto>>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IApplicationRepository _applicationRepository;

    private const string MethodName = nameof(GetApplicationsQueryHandler);

    public GetApplicationsQueryHandler(IMapper mapper,
                                       ILogger logger,
                                       IApplicationRepository applicationRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _applicationRepository = applicationRepository;
    }

    public async Task<ApiResult<List<ApplicationDto>>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - TourJobId: {request.TourJobId}");

        var applications = await _applicationRepository.GetApplicationsByTourJobIdAsync(request.TourJobId);

        var applicationsDto = _mapper.Map<List<ApplicationDto>>(applications);

        _logger.Information($"END {MethodName} - TourJobId: {request.TourJobId}");

        return new ApiSuccessResult<List<ApplicationDto>>(applicationsDto);
    }
}
