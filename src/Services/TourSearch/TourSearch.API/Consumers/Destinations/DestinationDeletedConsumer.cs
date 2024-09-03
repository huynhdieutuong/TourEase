using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using MassTransit;
using TourSearch.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Consumers.Destinations;

public class DestinationDeletedConsumer : IConsumer<DestinationDeleted>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IDestinationRepository _destinationRepository;

    public DestinationDeletedConsumer(IMapper mapper, ILogger logger, IDestinationRepository destinationRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _destinationRepository = destinationRepository;
    }

    public async Task Consume(ConsumeContext<DestinationDeleted> context)
    {
        _logger.Information("--> Consuming destination deleted: " + context.Message.Id);

        await _destinationRepository.DeleteByIdAsync(context.Message.Id);
    }
}
