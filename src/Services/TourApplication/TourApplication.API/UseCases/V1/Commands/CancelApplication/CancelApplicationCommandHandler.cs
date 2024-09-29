using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using TourApplication.API.Models;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class CancelApplicationCommandHandler : IRequestHandler<CancelApplicationCommand, ApiResult<bool>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ITourJobService _tourJobService;

    private const string MethodName = nameof(CancelApplicationCommandHandler);

    public CancelApplicationCommandHandler(IMapper mapper,
                                      ILogger logger,
                                      IApplicationRepository applicationRepository,
                                      ITourJobService tourJobService)
    {
        _mapper = mapper;
        _logger = logger;
        _applicationRepository = applicationRepository;
        _tourJobService = tourJobService;
    }

    public async Task<ApiResult<bool>> Handle(CancelApplicationCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Username: {request.Username}, ApplicationId: {request.ApplicationId}");

        var application = await _applicationRepository.GetApplicationByIdAsync(request.ApplicationId);
        CheckValidApplication(application, request);

        var tourJob = await _tourJobService.GetOrSaveTourJobAsync(application.TourJobId);
        _tourJobService.CheckValidTourJob(tourJob);

        var result = await _applicationRepository.CancelApplicationAsync(request.ApplicationId);
        if (!result)
        {
            throw new BadRequestException($"Failed to cancel the application with ID: {request.ApplicationId}");
        }

        _logger.Information($"END {MethodName} - Username: {request.Username}, ApplicationId: {request.ApplicationId}");

        return new ApiSuccessResult<bool>(result);
    }

    private static void CheckValidApplication(Application? application, CancelApplicationCommand request)
    {
        if (application == null)
        {
            throw new BadRequestException($"The application not found for ID: {request.ApplicationId}.");
        }

        if (application.TourGuide != request.Username)
        {
            throw new ForBidException("You are only allowed to cancel your application.");
        }

        if (application.Status != Status.Pending)
        {
            throw new BadRequestException("You can only cancel the pending application.");
        }
    }
}
