using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Shared.Exceptions;
using MassTransit;
using TourSearch.API.Entities;
using TourSearch.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Consumers.Applications;

public class TotalApplicantsUpdatedConsumer : IConsumer<TotalApplicantsUpdated>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourJobRepository _tourJobRepository;

    public TotalApplicantsUpdatedConsumer(IMapper mapper, ILogger logger, ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _tourJobRepository = tourJobRepository;
    }

    public async Task Consume(ConsumeContext<TotalApplicantsUpdated> context)
    {
        _logger.Information("--> TourSearch: Consuming total applicants updated - TourJobId: " + context.Message.TourJobId);

        var tourJobId = context.Message.TourJobId;
        var tourJob = await _tourJobRepository.FindByIdAsync(tourJobId);
        if (tourJob == null) throw new NotFoundException(nameof(TourJob), tourJobId);

        tourJob.TotalApplicants = context.Message.TotalApplicants;
        await _tourJobRepository.UpdateAsync(tourJob);
    }
}
