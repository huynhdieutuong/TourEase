using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Shared.Constants;
using BuildingBlocks.Shared.Exceptions;
using MassTransit;
using Serilog;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.Consumers.Applications;
public class TourJobFinishedConsumer : IConsumer<TourJobFinished>
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly ITourJobRepository _tourJobRepository;
    private readonly ITourUnitOfWork _tourUnitOfWork;

    public TourJobFinishedConsumer(ILogger logger, IMapper mapper, ITourJobRepository tourJobRepository, ITourUnitOfWork tourUnitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _tourJobRepository = tourJobRepository;
        _tourUnitOfWork = tourUnitOfWork;
    }

    public async Task Consume(ConsumeContext<TourJobFinished> context)
    {
        _logger.Information("--> Tour: Consuming tour job finished - TourJobId: " + context.Message.TourJob.Id);

        var tourJobId = context.Message.TourJob.Id;
        var tourJob = await _tourJobRepository.GetTourJobByIdAsync(tourJobId);
        if (tourJob == null) throw new NotFoundException(nameof(TourJob), tourJobId);

        tourJob.TourGuide = context.Message.TourGuide;
        tourJob.Status = TourJobStatus.Finished;
        _tourJobRepository.Update(tourJob);

        await _tourUnitOfWork.SaveChangesAsync();
    }
}
