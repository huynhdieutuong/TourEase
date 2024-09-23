using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tour.Application.Interfaces;
using Tour.Application.Services.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.UseCases.V1.Destinations;
public class DeleteDestinationCommandHandler : IRequestHandler<DeleteDestinationCommand>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITourUnitOfWork _tourUnitOfWork;
    private readonly IDestinationRepository _destinationRepository;
    private readonly ITourCacheService _tourCacheService;
    private readonly IDestinationService _destinationService;
    private readonly IPublishEndpoint _publishEndpoint;

    private const string MethodName = nameof(DeleteDestinationCommandHandler);

    public DeleteDestinationCommandHandler(IMapper mapper,
                                           ILogger logger,
                                           ITourUnitOfWork tourUnitOfWork,
                                           IDestinationRepository destinationRepository,
                                           ITourCacheService tourCacheService,
                                           IDestinationService destinationService,
                                           IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _logger = logger;
        _tourUnitOfWork = tourUnitOfWork;
        _destinationRepository = destinationRepository;
        _tourCacheService = tourCacheService;
        _destinationService = destinationService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(DeleteDestinationCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN {MethodName} Id: {request.Id}");

        var destination = await _destinationRepository.FindByIdAsync(request.Id);
        if (destination != null)
        {
            var destinations = await _tourCacheService.GetOrCreateDestinationsCacheAsync(
                async () => await _destinationRepository.FindAll().ToListAsync()
            );

            var lookup = destinations.ToLookup(d => d.ParentId);
            RecursiveRemoveChildren(lookup, destination.Id);

            _destinationRepository.Remove(destination);
        }

        await _publishEndpoint.Publish<DestinationDeleted>(new { request.Id });

        await _tourUnitOfWork.SaveChangesAsync();

        await _tourCacheService.InvalidDestinationsCacheAsync();

        _logger.Information($"END {MethodName} Id: {request.Id}");
    }

    private void RecursiveRemoveChildren(ILookup<Guid?, Destination> lookup, Guid parentId)
    {
        var children = lookup[parentId].ToList();

        if (children.Count == 0) return;

        foreach (var child in children)
        {
            RecursiveRemoveChildren(lookup, child.Id);
        }

        _destinationRepository.RemoveMultiple(children);
    }
}
