using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using MassTransit;
using TourSearch.API.Entities;
using TourSearch.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Consumers.Destinations;

public class DestinationCreatedConsumer : IConsumer<DestinationCreated>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IDestinationRepository _destinationRepository;

    public DestinationCreatedConsumer(IMapper mapper, ILogger logger, IDestinationRepository destinationRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _destinationRepository = destinationRepository;
    }

    public async Task Consume(ConsumeContext<DestinationCreated> context)
    {
        _logger.Information("--> TourSearch: Consuming destination created: " + context.Message.Id);

        var destination = _mapper.Map<Destination>(context.Message);

        await _destinationRepository.InsertAsync(destination);
    }
}
