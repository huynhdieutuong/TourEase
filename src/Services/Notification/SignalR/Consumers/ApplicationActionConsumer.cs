using BuildingBlocks.Messaging.Application;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using ILogger = Serilog.ILogger;

namespace SignalR.Consumers;

public class ApplicationActionConsumer : IConsumer<ApplicationAction>
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger _logger;

    public ApplicationActionConsumer(IHubContext<NotificationHub> hubContext, ILogger logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ApplicationAction> context)
    {
        _logger.Information("--> Application action message received. Type: " + context.Message.Type.ToString());

        await _hubContext.Clients.All.SendAsync("ApplicationAction", context.Message);
    }
}
