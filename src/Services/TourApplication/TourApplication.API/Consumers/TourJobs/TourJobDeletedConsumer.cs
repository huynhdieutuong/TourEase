using BuildingBlocks.Messaging.TourJob;
using MassTransit;
using TourApplication.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.Consumers.TourJobs;

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
        _logger.Information("--> TourApplication: Consuming tour job deleted - Id: " + context.Message.Id);

        await _tourJobRepository.DeleteTourJobAsync(context.Message.Id);
    }
}
