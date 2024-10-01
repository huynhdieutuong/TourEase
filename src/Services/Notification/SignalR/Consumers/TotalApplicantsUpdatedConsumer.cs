using BuildingBlocks.Messaging.Application;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using ILogger = Serilog.ILogger;

namespace SignalR.Consumers;

public class TotalApplicantsUpdatedConsumer : IConsumer<TotalApplicantsUpdated>
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger _logger;

    public TotalApplicantsUpdatedConsumer(IHubContext<NotificationHub> hubContext, ILogger logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TotalApplicantsUpdated> context)
    {
        _logger.Information("--> total applicants updated message received. Type: " + context.Message.Type.ToString());

        await _hubContext.Clients.All.SendAsync("TotalApplicantsUpdated", context.Message);
    }
}
