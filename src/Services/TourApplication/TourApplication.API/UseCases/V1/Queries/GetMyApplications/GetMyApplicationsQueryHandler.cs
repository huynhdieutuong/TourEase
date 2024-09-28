using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using MediatR;
using TourApplication.API.DTOs;
using TourApplication.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class GetMyApplicationsQueryHandler : IRequestHandler<GetMyApplicationsQuery, ApiResult<List<ApplicationDto>>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IApplicationRepository _applicationRepository;

    private const string MethodName = nameof(GetMyApplicationsQueryHandler);

    public GetMyApplicationsQueryHandler(IMapper mapper,
                                         ILogger logger,
                                         IApplicationRepository applicationRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _applicationRepository = applicationRepository;
    }

    public async Task<ApiResult<List<ApplicationDto>>> Handle(GetMyApplicationsQuery request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Username: {request.Username}");

        var applications = await _applicationRepository.GetMyApplications(request.Username);

        var applicationsDto = _mapper.Map<List<ApplicationDto>>(applications);

        _logger.Information($"END {MethodName} - Username: {request.Username}");

        return new ApiSuccessResult<List<ApplicationDto>>(applicationsDto);
    }
}
