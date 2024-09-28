using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using TourApplication.API.DTOs;
using TourApplication.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class ApplyTourJobCommandHandler : IRequestHandler<ApplyTourJobCommand, ApiResult<ApplicationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ITourJobRepository _tourJobRepository;

    private const string MethodName = nameof(ApplyTourJobCommandHandler);

    public ApplyTourJobCommandHandler(IMapper mapper,
                                      ILogger logger,
                                      IApplicationRepository applicationRepository,
                                      ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _applicationRepository = applicationRepository;
        _tourJobRepository = tourJobRepository;
    }

    public async Task<ApiResult<ApplicationDto>> Handle(ApplyTourJobCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Username: {request.Username}, TourJobId: {request.TourJobId}");

        var tourJob = await _tourJobRepository.GetTourJobByIdAsync(request.TourJobId);
        if (tourJob == null)
        {
            _logger.Warning($"Tour job not found for ID: {request.TourJobId}. Fetching from Tour Service...");
            // call to Tour Service to get and store
        }

        if (tourJob.Owner == request.Username)
        {
            throw new ForBidException("You cannot apply for your own job.");
        }

        if (tourJob.IsFinished || tourJob.ExpiredDate < DateTime.UtcNow)
        {
            throw new BadRequestException("The tour job has expired or finished.");
        }

        var hasAlreadyApplied = await HasTourGuideAlreadyApplied(request.TourJobId, request.Username);
        if (hasAlreadyApplied)
        {
            throw new BadRequestException("You can only apply for this job once.");
        }

        var id = await _applicationRepository.CreateApplicationAsync(request);

        var application = await _applicationRepository.GetApplicationByIdAsync(id);
        var applicationDto = _mapper.Map<ApplicationDto>(application);

        _logger.Information($"END {MethodName} - Username: {request.Username}, TourJobId: {request.TourJobId}");

        return new ApiSuccessResult<ApplicationDto>(applicationDto);
    }

    private async Task<bool> HasTourGuideAlreadyApplied(Guid tourJobId, string username)
    {
        var application = await _applicationRepository.GetApplicationByTourJobIdAndUsernameAsync(tourJobId, username);

        return application != null;
    }
}
