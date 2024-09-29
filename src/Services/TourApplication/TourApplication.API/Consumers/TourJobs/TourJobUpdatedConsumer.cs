using AutoMapper;
using BuildingBlocks.Messaging.TourJob;
using MassTransit;
using TourApplication.API.Models;
using TourApplication.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.Consumers.TourJobs;

public class TourJobUpdatedConsumer : IConsumer<TourJobUpdated>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourJobRepository _tourJobRepository;

    public TourJobUpdatedConsumer(IMapper mapper, ILogger logger, ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tourJobRepository = tourJobRepository;
    }

    public async Task Consume(ConsumeContext<TourJobUpdated> context)
    {
        _logger.Information("--> TourApplication: Consuming tour job updated - Id: " + context.Message.Id);

        var tourJob = _mapper.Map<TourJob>(context.Message);

        await _tourJobRepository.UpdateTourJobAsync(tourJob);
    }
}
