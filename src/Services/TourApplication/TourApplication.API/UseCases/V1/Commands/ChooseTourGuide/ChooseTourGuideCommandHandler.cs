using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using TourApplication.API.Models;
using TourApplication.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class ChooseTourGuideCommandHandler : IRequestHandler<ChooseTourGuideCommand, ApiResult<bool>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ITourJobRepository _tourJobRepository;

    private const string MethodName = nameof(ChooseTourGuideCommandHandler);

    public ChooseTourGuideCommandHandler(IMapper mapper,
                                      ILogger logger,
                                      IApplicationRepository applicationRepository,
                                      ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _applicationRepository = applicationRepository;
        _tourJobRepository = tourJobRepository;
    }

    public async Task<ApiResult<bool>> Handle(ChooseTourGuideCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Owner: {request.Owner}, ApplicationId: {request.ApplicationId}");

        var application = await _applicationRepository.GetApplicationByIdAsync(request.ApplicationId);
        if (application == null)
        {
            throw new BadRequestException($"The application not found for ID: {request.ApplicationId}.");
        }

        var tourJob = await _tourJobRepository.GetTourJobByIdAsync(application.TourJobId);
        if (tourJob == null)
        {
            _logger.Warning($"Tour job not found for ID: {application.TourJobId}. Fetching from Tour Service...");
            // call to Tour Service to get and store
        }

        if (tourJob.IsFinished || tourJob.ExpiredDate < DateTime.UtcNow)
        {
            throw new BadRequestException("The tour job has expired or finished.");
        }

        if (tourJob.Owner != request.Owner)
        {
            throw new ForBidException("You are only allowed to choose tour guide from your job.");
        }

        if (application.Status != Status.Pending)
        {
            throw new BadRequestException("You can only choose tour guide from the pending application.");
        }

        await _applicationRepository.ChooseTourGuideAsync(request.ApplicationId, tourJob.Id);

        // Todo: Update TourGuide in TourJob (Tour Service and Search Service)

        _logger.Information($"END {MethodName} - Owner: {request.Owner}, ApplicationId: {request.ApplicationId}");

        return new ApiSuccessResult<bool>(true);
    }
}
