using AutoMapper;
using BuildingBlocks.Messaging.TourJob;
using BuildingBlocks.Shared.ApiResult;
using MassTransit;
using MediatR;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.UseCases.V1.TourJobs;
public class CreateTourJobCommandHandler : IRequestHandler<CreateTourJobCommand, ApiResult<TourJobDto>>
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly ITourJobRepository _tourJobRepository;
    private readonly ITourDetailDestinationRepository _tourDetailDestinationRepository;
    private readonly ITourUnitOfWork _tourUnitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;

    private const string MethodName = nameof(CreateTourJobCommandHandler);

    public CreateTourJobCommandHandler(ILogger logger,
                                       IMapper mapper,
                                       ITourJobRepository tourJobRepository,
                                       ITourDetailDestinationRepository tourDetailDestinationRepository,
                                       ITourUnitOfWork tourUnitOfWork,
                                       IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _tourJobRepository = tourJobRepository;
        _tourDetailDestinationRepository = tourDetailDestinationRepository;
        _tourUnitOfWork = tourUnitOfWork;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ApiResult<TourJobDto>> Handle(CreateTourJobCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Tour Job Title: {request.Title}");

        var tourJob = _mapper.Map<TourJob>(request);
        _tourJobRepository.Add(tourJob);

        var tourDetailDestinations = request.DestinationIds.Select(destinationId
                                                => new TourDetailDestination
                                                {
                                                    DestinationId = destinationId,
                                                    TourDetailId = tourJob.Detail.Id
                                                }).ToList();
        _tourDetailDestinationRepository.AddMultiple(tourDetailDestinations);

        var tourJobDto = _mapper.Map<TourJobDto>(tourJob);
        await _publishEndpoint.Publish(_mapper.Map<TourJobCreated>(tourJobDto));

        await _tourUnitOfWork.SaveChangesAsync();

        _logger.Information($"END {MethodName} - Tour Job Title: {request.Title}");
        return new ApiSuccessResult<TourJobDto>(tourJobDto);
    }
}
