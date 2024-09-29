using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using TourApplication.API.Models;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class ReApplyApplicationCommandHandler : IRequestHandler<ReApplyApplicationCommand, ApiResult<bool>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ITourJobService _tourJobService;
    private readonly IApplicationService _applicationService;

    private const string MethodName = nameof(ReApplyApplicationCommandHandler);

    public ReApplyApplicationCommandHandler(IMapper mapper,
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

    public async Task<ApiResult<bool>> Handle(ReApplyApplicationCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Username: {request.Username}, ApplicationId: {request.ApplicationId}");

        var application = await _applicationRepository.GetApplicationByIdAsync(request.ApplicationId);
        CheckValidApplication(application, request);

        var tourJob = await _tourJobService.GetOrSaveTourJobAsync(application.TourJobId);
        _tourJobService.CheckValidTourJob(tourJob);

        var result = await _applicationRepository.ReApplyApplicationAsync(request.ApplicationId);
        if (!result)
        {
            throw new BadRequestException($"Failed to reapply the application with ID: {request.ApplicationId}");
        }

        await _applicationService.PublishTotalApplicantsUpdated(tourJob.Id);

        _logger.Information($"END {MethodName} - Username: {request.Username}, ApplicationId: {request.ApplicationId}");

        return new ApiSuccessResult<bool>(result);
    }

    private static void CheckValidApplication(Application? application, ReApplyApplicationCommand request)
    {
        if (application == null)
        {
            throw new BadRequestException($"The application not found for ID: {request.ApplicationId}.");
        }

        if (application.TourGuide != request.Username)
        {
            throw new ForBidException("You are only allowed to reapply your application.");
        }

        if (application.Status != Status.Canceled)
        {
            throw new BadRequestException("You can only reapply the canceled application.");
        }
    }
}
