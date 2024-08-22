using AutoMapper;
using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Shared.ApiResult;
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
    private readonly ISlugService _slugService;

    private const string MethodName = nameof(CreateTourJobCommandHandler);

    public CreateTourJobCommandHandler(ILogger logger,
                                       IMapper mapper,
                                       ITourJobRepository tourJobRepository,
                                       ITourDetailDestinationRepository tourDetailDestinationRepository,
                                       ITourUnitOfWork tourUnitOfWork,
                                       ISlugService slugService)
    {
        _logger = logger;
        _mapper = mapper;
        _tourJobRepository = tourJobRepository;
        _tourDetailDestinationRepository = tourDetailDestinationRepository;
        _tourUnitOfWork = tourUnitOfWork;
        _slugService = slugService;
    }

    public async Task<ApiResult<TourJobDto>> Handle(CreateTourJobCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} - Tour Job Title: {request.Title}");

        var tourJob = _mapper.Map<TourJob>(request);
        tourJob.Slug = _slugService.GenerateTourJobSlug(tourJob.Title);
        _tourJobRepository.Add(tourJob);

        var tourDetailDestinations = request.DestinationIds.Select(destinationId
                                                => new TourDetailDestination
                                                {
                                                    DestinationId = destinationId,
                                                    TourDetailId = tourJob.Detail.Id
                                                }).ToList();
        _tourDetailDestinationRepository.AddMultiple(tourDetailDestinations);

        await _tourUnitOfWork.SaveChangesAsync();

        _logger.Information($"END {MethodName} - Tour Job Title: {request.Title}");
        return new ApiSuccessResult<TourJobDto>(_mapper.Map<TourJobDto>(tourJob));
    }
}
