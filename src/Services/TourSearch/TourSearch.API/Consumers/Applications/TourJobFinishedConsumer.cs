using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Shared.Constants;
using BuildingBlocks.Shared.Exceptions;
using MassTransit;
using TourSearch.API.Entities;
using TourSearch.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Consumers.Applications;

public class TourJobFinishedConsumer : IConsumer<TourJobFinished>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourJobRepository _tourJobRepository;

    public TourJobFinishedConsumer(IMapper mapper, ILogger logger, ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tourJobRepository = tourJobRepository;
    }

    public async Task Consume(ConsumeContext<TourJobFinished> context)
    {
        _logger.Information("--> TourSearch: Consuming tour job finished - TourJobId: " + context.Message.TourJobId);

        var tourJobId = context.Message.TourJobId;
        var tourJob = await _tourJobRepository.FindByIdAsync(tourJobId);
        if (tourJob == null) throw new NotFoundException(nameof(TourJob), tourJobId);

        tourJob.TourGuide = context.Message.TourGuide;
        tourJob.Status = TourJobStatus.Finished.ToString();
        await _tourJobRepository.UpdateAsync(tourJob);
    }
}
