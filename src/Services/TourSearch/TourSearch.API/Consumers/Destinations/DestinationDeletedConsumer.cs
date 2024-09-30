using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using MassTransit;
using TourSearch.API.Entities;
using TourSearch.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourSearch.API.Consumers.Destinations;

public class DestinationDeletedConsumer : IConsumer<DestinationDeleted>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IDestinationRepository _destinationRepository;
    private readonly ITourJobRepository _tourJobRepository;

    public DestinationDeletedConsumer(IMapper mapper, ILogger logger, IDestinationRepository destinationRepository, ITourJobRepository tourJobRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _destinationRepository = destinationRepository;
        _tourJobRepository = tourJobRepository;
    }

    public async Task Consume(ConsumeContext<DestinationDeleted> context)
    {
        _logger.Information("--> TourSearch: Consuming destination deleted: " + context.Message.Id);

        _logger.Information("1. Get a list of descendant destinations to delete");
        var destinations = await _destinationRepository.FindAllAsync();
        var deletedDestinationIds = GetDescendantIds(destinations, context.Message.Id)
                                        .Append(context.Message.Id)
                                        .ToList();

        _logger.Information("2. Update the DestinationIds of the tour jobs that contain the list of deleted destinations");
        //var tourJobs = await _tourJobRepository.FindAllAsync(tj => tj.DestinationIds.Any(id => deletedDestinationIds.Contains(id)));
        var tourJobs = await _tourJobRepository.GetTourJobsByDestinationIds(deletedDestinationIds);
        if (tourJobs.Any())
        {
            var updatedCount = await _tourJobRepository.UpdateDeletedDestinationsInTourJobs(tourJobs, deletedDestinationIds);
            _logger.Information($"Updated {updatedCount} TourJobs.");
        }

        _logger.Information("3. Delete the list of descendant destinations and the destination");
        await _destinationRepository.DeleteManyAsync(x => deletedDestinationIds.Contains(x.Id));
    }

    private List<Guid> GetDescendantIds(List<Destination> destinations, Guid parentId)
    {
        var result = new List<Guid>();
        Recursive(parentId);

        void Recursive(Guid parentId)
        {
            var children = destinations.Where(d => d.ParentId == parentId);
            foreach (var child in children)
            {
                result.Add(child.Id);
                Recursive(child.Id);
            }
        }

        return result;
    }
}
