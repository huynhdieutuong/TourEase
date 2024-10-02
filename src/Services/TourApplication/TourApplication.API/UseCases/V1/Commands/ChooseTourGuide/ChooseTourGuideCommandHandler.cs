using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MassTransit;
using MediatR;
using TourApplication.API.Models;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class ChooseTourGuideCommandHandler : IRequestHandler<ChooseTourGuideCommand, ApiResult<bool>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ITourJobService _tourJobService;
    private readonly IPublishEndpoint _publishEndpoint;

    private const string MethodName = nameof(ChooseTourGuideCommandHandler);

    public ChooseTourGuideCommandHandler(IMapper mapper,
                                      ILogger logger,
                                      IApplicationRepository applicationRepository,
                                      ITourJobService tourJobService,
                                      IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _logger = logger;
        _applicationRepository = applicationRepository;
        _tourJobService = tourJobService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ApiResult<bool>> Handle(ChooseTourGuideCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Owner: {request.Owner}, ApplicationId: {request.ApplicationId}");

        var application = await _applicationRepository.GetApplicationByIdAsync(request.ApplicationId);
        CheckValidApplication(application, request);

        var tourJob = await _tourJobService.GetOrSaveTourJobAsync(application.TourJobId);
        _tourJobService.CheckValidTourJob(tourJob);

        if (tourJob.Owner != request.Owner)
        {
            throw new ForBidException("You are only allowed to choose tour guide from your job.");
        }

        await _applicationRepository.ChooseTourGuideAsync(request.ApplicationId, tourJob.Id);

        await _publishEndpoint.Publish(new TourJobFinished
        {
            TourJob = _mapper.Map<TourJobMessage>(tourJob),
            TourGuide = application.TourGuide
        });

        _logger.Information($"END {MethodName} - Owner: {request.Owner}, ApplicationId: {request.ApplicationId}");

        return new ApiSuccessResult<bool>(true);
    }

    private static void CheckValidApplication(Application application, ChooseTourGuideCommand request)
    {
        if (application == null)
        {
            throw new BadRequestException($"The application not found for ID: {request.ApplicationId}.");
        }

        if (application.Status != Status.Pending)
        {
            throw new BadRequestException("You can only choose tour guide from the pending application.");
        }
    }
}
