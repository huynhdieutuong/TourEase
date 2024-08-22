using AutoMapper;
using BuildingBlocks.Contracts.Services;
using Tour.Application.UseCases.V1.TourJobs;
using Tour.Domain.Entities;

namespace Tour.Application.Mapping;
public class SlugResolver : IMappingAction<UpdateTourJobCommand, TourJob>
{
    private readonly ISlugService _slugService;

    public SlugResolver(ISlugService slugService)
    {
        _slugService = slugService;
    }

    public void Process(UpdateTourJobCommand source, TourJob destination, ResolutionContext context)
    {
        if (source.Title != destination.Title)
        {
            destination.Slug = _slugService.GenerateTourJobSlug(source.Title);
        }
    }
}
