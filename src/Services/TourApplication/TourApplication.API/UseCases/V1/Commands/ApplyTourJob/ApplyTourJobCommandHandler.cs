using AutoMapper;
using BuildingBlocks.Messaging.Enums;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using TourApplication.API.DTOs;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class ApplyTourJobCommandHandler : IRequestHandler<ApplyTourJobCommand, ApiResult<ApplicationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourJobService _tourJobService;
    private readonly IApplicationService _applicationService;
    private readonly IApplicationRepository _applicationRepository;

    private const string MethodName = nameof(ApplyTourJobCommandHandler);

    public ApplyTourJobCommandHandler(IMapper mapper,
                                      ILogger logger,
                                      IApplicationRepository applicationRepository,
                                      ITourJobService tourJobService,

                                      IApplicationService applicationService)
    {
        _mapper = mapper;
        _logger = logger;
        _applicationRepository = applicationRepository;
        _tourJobService = tourJobService;
        _applicationService = applicationService;
    }

    public async Task<ApiResult<ApplicationDto>> Handle(ApplyTourJobCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Username: {request.Username}, TourJobId: {request.TourJobId}");

        var tourJob = await _tourJobService.GetOrSaveTourJobAsync(request.TourJobId);
        _tourJobService.CheckValidTourJob(tourJob);

        if (tourJob.Owner == request.Username)
        {
            throw new ForBidException("You cannot apply for your own job.");
        }

        await HasTourGuideAlreadyApplied(request.TourJobId, request.Username);

        var id = await _applicationRepository.CreateApplicationAsync(request);

        await _applicationService.PublishTotalApplicantsUpdated(tourJob.Id, ApplicationTypes.New);

        var application = await _applicationRepository.GetApplicationByIdAsync(id);
        var applicationDto = _mapper.Map<ApplicationDto>(application);

        _logger.Information($"END {MethodName} - Username: {request.Username}, TourJobId: {request.TourJobId}");

        return new ApiSuccessResult<ApplicationDto>(applicationDto);
    }

    private async Task HasTourGuideAlreadyApplied(Guid tourJobId, string username)
    {
        var application = await _applicationRepository.GetApplicationByTourJobIdAndUsernameAsync(tourJobId, username);

        if (application != null)
        {
            throw new BadRequestException("You can only apply for this job once.");
        }
    }
}
