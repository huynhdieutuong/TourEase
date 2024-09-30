using BuildingBlocks.Shared.Behaviors;
using BuildingBlocks.Shared.Configurations;
using BuildingBlocks.Shared.Extensions;
using FluentValidation;
using MassTransit;
using MediatR;
using System.Reflection;
using TourApplication.API.Consumers.TourJobs;
using TourApplication.API.Persistence;
using TourApplication.API.Persistence.Interfaces;
using TourApplication.API.Repositories;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.Services;
using TourApplication.API.Services.Interfaces;

namespace TourApplication.API.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnectionFactory>(sp =>
        {
            var connectionString = configuration.GetConnectionString("MySqlConnection") ?? throw new ArgumentNullException("Tour Application connection string is not configured.");
            return new MySqlConnectionFactory(connectionString);
        });

        services.AddScoped<IDbMigrationService, DbMigrationService>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<ITourJobRepository, TourJobRepository>();
    }

    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<ITourJobService, TourJobService>();
        services.AddScoped<IGrpcTourJobClient, GrpcTourJobClient>();

        services.AddHostedService<CheckTourJobExpired>();

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

            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("application"));

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri(settings.HostAddress));
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}
