using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using BuildingBlocks.Messaging.TourJob;
using TourSearch.API.Entities;

namespace TourSearch.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TourJobCreated, TourJob>();

        CreateMap<DestinationCreated, Destination>();
    }
}
