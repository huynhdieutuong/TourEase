using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Shared.Constants;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tour.Application.Interfaces;

namespace Tour.Application.Consumers.Applications;
public class TourJobsExpiredConsumer : IConsumer<TourJobsExpired>
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly ITourJobRepository _tourJobRepository;
    private readonly ITourUnitOfWork _tourUnitOfWork;

    public TourJobsExpiredConsumer(ILogger logger, IMapper mapper, ITourJobRepository tourJobRepository, ITourUnitOfWork tourUnitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _tourJobRepository = tourJobRepository;
        _tourUnitOfWork = tourUnitOfWork;
    }

    public async Task Consume(ConsumeContext<TourJobsExpired> context)
    {
        _logger.Information("--> Tour: Consuming tour jobs expired - TourJobIds: " + context.Message.TourJobIds);

        var tourJobIds = context.Message.TourJobIds;
        var tourJobs = await _tourJobRepository.FindAll(x => tourJobIds.Contains(x.Id)).ToListAsync();
        _logger.Information("Tour: Found {count} tour jobs that have expired", tourJobs.Count);
        if (tourJobs.Count == 0) return;

        tourJobs.ForEach(tourJob => tourJob.Status = TourJobStatus.Expired);
        _tourJobRepository.UpdateMultiple(tourJobs);
        await _tourUnitOfWork.SaveChangesAsync();
    }
}
