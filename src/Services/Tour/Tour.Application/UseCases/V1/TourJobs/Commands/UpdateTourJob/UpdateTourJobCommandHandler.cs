using AutoMapper;
using BuildingBlocks.Messaging.TourJob;
using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Exceptions;
using MassTransit;
using MediatR;
using Serilog;
using Tour.Application.DTOs;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.UseCases.V1.TourJobs.Commands.UpdateTourJob;
public class UpdateTourJobCommandHandler : IRequestHandler<UpdateTourJobCommand, ApiResult<TourJobDto>>
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly ITourUnitOfWork _tourUnitOfWork;
    private readonly ITourJobRepository _tourJobRepository;
    private readonly ITourDetailDestinationRepository _tourDetailDestinationRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    private const string MethodName = nameof(UpdateTourJobCommandHandler);

    public UpdateTourJobCommandHandler(ILogger logger,
                                       IMapper mapper,
                                       ITourUnitOfWork tourUnitOfWork,
                                       ITourJobRepository tourJobRepository,
                                       ITourDetailDestinationRepository tourDetailDestinationRepository,
                                       IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _tourUnitOfWork = tourUnitOfWork;
        _tourJobRepository = tourJobRepository;
        _tourDetailDestinationRepository = tourDetailDestinationRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ApiResult<TourJobDto>> Handle(UpdateTourJobCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Tour Job Title: {request.Title}");

        var tourJob = await _tourJobRepository.FindByIdAsync(request.Id, tj => tj.Detail.TourDetailDestinations);
        if (tourJob == null) throw new NotFoundException(nameof(TourJob), request.Id);

        _mapper.Map(request, tourJob);
        _tourJobRepository.Update(tourJob);

        UpdateTourDetailDestinations(tourJob.Detail, request.DestinationIds);

        var tourJobDto = _mapper.Map<TourJobDto>(tourJob);
        await _publishEndpoint.Publish(_mapper.Map<TourJobUpdated>(tourJobDto));

        await _tourUnitOfWork.SaveChangesAsync();

        _logger.Information($"END {MethodName} - Tour Job Title: {request.Title}");
        return new ApiSuccessResult<TourJobDto>(tourJobDto);
    }

    private void UpdateTourDetailDestinations(TourDetail tourDetail, List<Guid> requestDestinationIds)
    {
        var currentDestinationIds = tourDetail.TourDetailDestinations.Select(x => (Guid)x.DestinationId).ToList();

        var tourDetailDestinationsToAdd = requestDestinationIds.Except(currentDestinationIds)
                                                                .Select(destinationId => new TourDetailDestination
                                                                {
                                                                    DestinationId = destinationId,
                                                                    TourDetailId = tourDetail.Id
                                                                }).ToList();

        var tourDetailDestinationsToRemove = tourDetail.TourDetailDestinations
                                                .Where(cd => !requestDestinationIds.Contains((Guid)cd.DestinationId)).ToList();

        _tourDetailDestinationRepository.AddMultiple(tourDetailDestinationsToAdd);
        _tourDetailDestinationRepository.RemoveMultiple(tourDetailDestinationsToRemove);
    }
}
