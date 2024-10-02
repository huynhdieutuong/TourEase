using BuildingBlocks.Messaging.Application;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using ILogger = Serilog.ILogger;

namespace SignalR.Consumers;

public class TourJobFinishedConsumer : IConsumer<TourJobFinished>
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger _logger;

    public TourJobFinishedConsumer(IHubContext<NotificationHub> hubContext, ILogger logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TourJobFinished> context)
    {
        _logger.Information("--> Tour job finished message received");

        await _hubContext.Clients.All.SendAsync("TourJobFinished", context.Message);
    }
}
