using BuildingBlocks.Shared.Configurations;
using BuildingBlocks.Shared.Extensions;
using MassTransit;
using SignalR.Consumers;

namespace SignalR.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.ConfigureMassTransit();
    }

    public static void ConfigureMassTransit(this IServiceCollection services)
    {
        var settings = services.GetOptions<EventBusSettings>(nameof(EventBusSettings));
        if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
        {
            throw new ArgumentException("EventBusSetting is not configured");
        }

        services.AddMassTransit(x =>
        {
            x.AddConsumersFromNamespaceContaining<TourJobCreatedConsumer>();

            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("nt"));

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri(settings.HostAddress));
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}
