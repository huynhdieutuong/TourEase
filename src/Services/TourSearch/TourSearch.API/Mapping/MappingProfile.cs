using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using BuildingBlocks.Messaging.TourJob;
using TourSearch.API.Entities;

namespace TourSearch.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TourJobCreated, TourJob>()
            .ForMember(dest => dest.ExpiredDate,
                opt => opt.MapFrom(src => src.ExpiredDate.DateTime))
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.DateTime))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.DateTime))
            .ForMember(dest => dest.CreatedDate,
                opt => opt.MapFrom(src => src.CreatedDate.DateTime))
            .ForMember(dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => src.UpdatedDate.DateTime))
            .ForMember(dest => dest.DeletedDate,
                opt => opt.MapFrom(src => src.DeletedDate.HasValue ? src.DeletedDate.Value.DateTime : (DateTime?)null));

        CreateMap<TourJobUpdated, TourJob>()
            .ForMember(dest => dest.ExpiredDate,
                opt => opt.MapFrom(src => src.ExpiredDate.DateTime))
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.DateTime))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.DateTime))
            .ForMember(dest => dest.CreatedDate,
                opt => opt.MapFrom(src => src.CreatedDate.DateTime))
            .ForMember(dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => src.UpdatedDate.DateTime))
            .ForMember(dest => dest.DeletedDate,
                opt => opt.MapFrom(src => src.DeletedDate.HasValue ? src.DeletedDate.Value.DateTime : (DateTime?)null));

        CreateMap<DestinationCreated, Destination>();
        CreateMap<DestinationUpdated, Destination>();
    }
}
