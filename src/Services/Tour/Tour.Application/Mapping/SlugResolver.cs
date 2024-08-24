using AutoMapper;
using BuildingBlocks.Contracts.Services;
using Tour.Application.UseCases.V1.TourJobs;
using Tour.Domain.Entities;

namespace Tour.Application.Mapping;
public class SlugResolver<T> : IMappingAction<T, TourJob> where T : CreateOrUpdateCommand
{
    private readonly ISlugService _slugService;

    public SlugResolver(ISlugService slugService)
    {
        _slugService = slugService;
    }

    public void Process(T source, TourJob destination, ResolutionContext context)
    {
        if (source.GetType() == typeof(CreateTourJobCommand))
        {
            destination.Slug = _slugService.GenerateTourJobSlug(source.Title);
        }

        if (source.GetType() == typeof(UpdateTourJobCommand) && source.Title != destination.Title)
        {
            destination.Slug = _slugService.GenerateTourJobSlug(source.Title);
        }
    }
}
