using AutoMapper;
using BuildingBlocks.Messaging.TourJob;
using BuildingBlocks.Shared.Exceptions;
using MassTransit;
using Serilog;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.Consumers.TourJobs;
public class TourJobCreatedFaultConsumer : IConsumer<Fault<TourJobCreated>>
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly ITourJobRepository _tourJobRepository;
    private readonly ITourUnitOfWork _tourUnitOfWork;

    public TourJobCreatedFaultConsumer(ILogger logger, ITourJobRepository tourJobRepository, IMapper mapper, ITourUnitOfWork tourUnitOfWork)
    {
        _logger = logger;
        _tourJobRepository = tourJobRepository;
        _mapper = mapper;
        _tourUnitOfWork = tourUnitOfWork;
    }

    public async Task Consume(ConsumeContext<Fault<TourJobCreated>> context)
    {
        _logger.Information("-->Tour: Consuming faulty creation");

        var exception = context.Message.Exceptions.First();
        if (exception.ExceptionType == "System.ArgumentException")
        {
            var tourJobId = context.Message.Message.Id;
            var tourJob = await _tourJobRepository.GetTourJobByIdAsync(tourJobId);
            if (tourJob == null) throw new NotFoundException(nameof(TourJob), tourJobId);

            tourJob.Title = context.Message.Message.Title = "Tour job test";
            _tourJobRepository.Update(tourJob);

            await context.Publish(context.Message.Message);

            await _tourUnitOfWork.SaveChangesAsync();
        }
        else
        {
            _logger.Information("Not an argument exception - update error dashboard somewhere");
        }
    }
}
