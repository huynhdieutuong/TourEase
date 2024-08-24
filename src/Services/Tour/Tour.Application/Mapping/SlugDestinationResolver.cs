using AutoMapper;
using BuildingBlocks.Contracts.Services;
using Tour.Application.UseCases.V1.Destinations;
using Tour.Domain.Entities;

namespace Tour.Application.Mapping;
public class SlugDestinationResolver<T> : IMappingAction<T, Destination> where T : CreateOrUpdateDestinationCommand
{
    private readonly ISlugService _slugService;

    public SlugDestinationResolver(ISlugService slugService)
    {
        _slugService = slugService;
    }

    public void Process(T source, Destination destination, ResolutionContext context)
    {
        if (source.GetType() == typeof(CreateDestinationCommand))
        {
            destination.Slug = _slugService.GenerateDestinationSlug(source.Name);
        }

        if (source.GetType() == typeof(UpdateDestinationCommand) && source.Name != destination.Name)
        {
            destination.Slug = _slugService.GenerateDestinationSlug(source.Name);
        }
    }
}
