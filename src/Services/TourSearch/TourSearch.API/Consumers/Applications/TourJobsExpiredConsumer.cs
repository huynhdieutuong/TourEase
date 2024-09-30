using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Shared.Constants;
using MassTransit;
using MongoDB.Driver;
using TourSearch.API.Entities;
using TourSearch.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Consumers.Applications;

public class TourJobsExpiredConsumer : IConsumer<TourJobsExpired>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourJobRepository _tourJobRepository;

    public TourJobsExpiredConsumer(IMapper mapper, ILogger logger, ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tourJobRepository = tourJobRepository;
    }

    public async Task Consume(ConsumeContext<TourJobsExpired> context)
    {
        _logger.Information("--> TourSearch: Consuming tour jobs expired - TourJobIds: " + context.Message.TourJobIds);

        var tourJobIds = context.Message.TourJobIds;
        var tourJobs = await _tourJobRepository.FindAllAsync(x => tourJobIds.Contains(x.Id));
        _logger.Information("TourSearch: Found {count} tour jobs that have expired", tourJobs.Count);
        if (tourJobs.Count == 0) return;

        var filter = Builders<TourJob>.Filter.Where(x => tourJobIds.Contains(x.Id));
        var update = Builders<TourJob>.Update.Set(x => x.Status, TourJobStatus.Expired.ToString());

        await _tourJobRepository.UpdateManyAsync(filter, update);
    }
}
