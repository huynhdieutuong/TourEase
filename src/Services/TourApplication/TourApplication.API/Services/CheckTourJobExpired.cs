using BuildingBlocks.Messaging.Application;
using MassTransit;
using TourApplication.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.Services;

public class CheckTourJobExpired : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _services;

    public CheckTourJobExpired(ILogger logger,
                               IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Information("Starting check for expired tour jobs");

        stoppingToken.Register(() => _logger.Information("===> Expired tour job check is stopping"));

        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckTourJobs(stoppingToken);

            await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task CheckTourJobs(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();

        // 1. Update IsFinished status in TourApplication Service
        var tourJobRepository = scope.ServiceProvider.GetRequiredService<ITourJobRepository>();
        var expiredTourJobIds = await tourJobRepository.GetExpiredTourJobIdsAsync();

        _logger.Information("Application: Found {count} tour jobs that have expired", expiredTourJobIds.Count);

        if (expiredTourJobIds.Count == 0) return;

        var affectedRows = await tourJobRepository.SetExpiredTourJobsToFinishedAsync(expiredTourJobIds);

        _logger.Information("Application: Set {count} tour jobs as expired", affectedRows);

        // 2. Publish TourJobsExpired event to update TourJob status to Expired
        var endpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        await endpoint.Publish(new TourJobsExpired
        {
            TourJobIds = expiredTourJobIds
        }, stoppingToken);
    }
}
