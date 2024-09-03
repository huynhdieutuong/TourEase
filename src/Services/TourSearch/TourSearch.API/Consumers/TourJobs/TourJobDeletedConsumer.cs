using BuildingBlocks.Messaging.TourJob;
using MassTransit;
using TourSearch.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Consumers.TourJobs;

public class TourJobDeletedConsumer : IConsumer<TourJobDeleted>
{
    private readonly ILogger _logger;
    private readonly ITourJobRepository _tourJobRepository;

    public TourJobDeletedConsumer(ILogger logger, ITourJobRepository tourJobRepository)
    {
        _logger = logger;
        _tourJobRepository = tourJobRepository;
    }

    public async Task Consume(ConsumeContext<TourJobDeleted> context)
    {
        _logger.Information("--> Consuming tour job deleted: " + context.Message.Id);

        await _tourJobRepository.DeleteByIdAsync(context.Message.Id);
    }
}
