using AutoMapper;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MediatR;
using TourApplication.API.Models;
using TourApplication.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.UseCases.V1;

public class ReApplyApplicationCommandHandler : IRequestHandler<ReApplyApplicationCommand, ApiResult<bool>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ITourJobRepository _tourJobRepository;

    private const string MethodName = nameof(ReApplyApplicationCommandHandler);

    public ReApplyApplicationCommandHandler(IMapper mapper,
                                      ILogger logger,
                                      IApplicationRepository applicationRepository,
                                      ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _applicationRepository = applicationRepository;
        _tourJobRepository = tourJobRepository;
    }

    public async Task<ApiResult<bool>> Handle(ReApplyApplicationCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Username: {request.Username}, ApplicationId: {request.ApplicationId}");

        var application = await _applicationRepository.GetApplicationByIdAsync(request.ApplicationId);
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

        var result = await _applicationRepository.ReApplyApplicationAsync(request.ApplicationId);
        if (!result)
        {
            throw new BadRequestException($"Failed to reapply the application with ID: {request.ApplicationId}");
        }

        _logger.Information($"END {MethodName} - Username: {request.Username}, ApplicationId: {request.ApplicationId}");

        return new ApiSuccessResult<bool>(result);
    }
}
