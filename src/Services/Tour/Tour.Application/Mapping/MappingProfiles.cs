using AutoMapper;
using BuildingBlocks.Shared.Extensions;
using Tour.Application.DTOs;
using Tour.Application.UseCases.V1.Destinations;
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
            .ForMember(dest => dest.DestinationIds,
                       opt => opt.MapFrom(src => src.TourDetailDestinations.Select(tdd => tdd.DestinationId)));

        CreateMap<CreateTourJobCommand, TourJob>()
            .BeforeMap<SlugResolver<CreateTourJobCommand>>()
            .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src));
        CreateMap<CreateTourJobCommand, TourDetail>();

        CreateMap<UpdateTourJobCommand, TourJob>()
            .BeforeMap<SlugResolver<UpdateTourJobCommand>>()
            .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src));
        CreateMap<UpdateTourJobCommand, TourDetail>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        #endregion

        #region Destination
        CreateMap<Destination, DestinationDto>().ReverseMap();

        CreateMap<CreateDestinationCommand, Destination>()
            .BeforeMap<SlugDestinationResolver<CreateDestinationCommand>>();

        CreateMap<UpdateDestinationCommand, Destination>()
            .BeforeMap<SlugDestinationResolver<UpdateDestinationCommand>>();
        #endregion
    }
}
