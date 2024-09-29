using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Shared.Exceptions;
using MassTransit;
using Serilog;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.Consumers.Applications;
public class TotalApplicantsUpdatedConsumer : IConsumer<TotalApplicantsUpdated>
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly ITourJobRepository _tourJobRepository;
    private readonly ITourUnitOfWork _tourUnitOfWork;

    public TotalApplicantsUpdatedConsumer(ILogger logger, IMapper mapper, ITourJobRepository tourJobRepository, ITourUnitOfWork tourUnitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _tourJobRepository = tourJobRepository;
        _tourUnitOfWork = tourUnitOfWork;
    }

    public async Task Consume(ConsumeContext<TotalApplicantsUpdated> context)
    {
        _logger.Information("--> Tour: Consuming total applicants updated - TourJobId: " + context.Message.TourJobId);

        var tourJobId = context.Message.TourJobId;
        var tourJob = await _tourJobRepository.GetTourJobByIdAsync(tourJobId);
        if (tourJob == null) throw new NotFoundException(nameof(TourJob), tourJobId);

        tourJob.TotalApplicants = context.Message.TotalApplicants;
        _tourJobRepository.Update(tourJob);

        await _tourUnitOfWork.SaveChangesAsync();
    }
}
