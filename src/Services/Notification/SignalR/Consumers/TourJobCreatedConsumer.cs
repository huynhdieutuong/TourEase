using BuildingBlocks.Messaging.TourJob;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using ILogger = Serilog.ILogger;

namespace SignalR.Consumers;

public class TourJobCreatedConsumer : IConsumer<TourJobCreated>
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger _logger;

    public TourJobCreatedConsumer(IHubContext<NotificationHub> hubContext, ILogger logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TourJobCreated> context)
    {
        _logger.Information("--> Tour job created message received");

        await _hubContext.Clients.All.SendAsync("TourJobCreated", context.Message);
    }
}
