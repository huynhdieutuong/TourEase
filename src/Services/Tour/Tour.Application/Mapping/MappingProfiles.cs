using AutoMapper;
using BuildingBlocks.Shared.Extensions;
using Tour.Application.DTOs;
using Tour.Application.UseCases.V1.TourJobs;
using Tour.Domain.Entities;

namespace Tour.Application.Mapping;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region Tour Job
        CreateMap<TourJob, TourJobDto>()
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.GetEnumDescription()))
            .IncludeMembers(x => x.Detail);
        CreateMap<TourDetail, TourJobDto>()
            .ForMember(dest => dest.Destinations,
                       opt => opt.MapFrom(src => src.TourDetailDestinations.Select(tdd => tdd.Destination)));

        CreateMap<CreateTourJobCommand, TourJob>()
            .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src));
        CreateMap<CreateTourJobCommand, TourDetail>();

        CreateMap<UpdateTourJobCommand, TourJob>()
            .BeforeMap<SlugResolver>()
            .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src));
        CreateMap<UpdateTourJobCommand, TourDetail>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        #endregion

        #region Destination
        CreateMap<Destination, DestinationDto>().ReverseMap();
        #endregion
    }
}
