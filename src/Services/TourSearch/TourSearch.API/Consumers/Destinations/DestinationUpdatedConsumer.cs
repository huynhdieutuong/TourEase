using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using MassTransit;
using TourSearch.API.Entities;
using TourSearch.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Consumers.Destinations;

public class DestinationUpdatedConsumer : IConsumer<DestinationUpdated>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IDestinationRepository _destinationRepository;

    public DestinationUpdatedConsumer(IMapper mapper, ILogger logger, IDestinationRepository destinationRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _destinationRepository = destinationRepository;
    }

    public async Task Consume(ConsumeContext<DestinationUpdated> context)
    {
        _logger.Information("--> TourSearch: Consuming destination updated: " + context.Message.Id);

        var destination = _mapper.Map<Destination>(context.Message);

        await _destinationRepository.UpdateAsync(destination);
    }
}
