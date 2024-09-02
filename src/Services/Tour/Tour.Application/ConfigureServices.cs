using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Infrastructure.Services;
using BuildingBlocks.Shared.Behaviors;
using BuildingBlocks.Shared.Configurations;
using BuildingBlocks.Shared.Extensions;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tour.Application.Services;
using Tour.Application.Services.Interfaces;

namespace Tour.Application;
public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<ISlugService, SlugService>();
        services.AddScoped<IDestinationService, DestinationService>();

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
            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("tour"));

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri(settings.HostAddress));
                //cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}
