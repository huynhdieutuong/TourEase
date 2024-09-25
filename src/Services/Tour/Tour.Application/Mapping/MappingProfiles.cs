using AutoMapper;
using BuildingBlocks.Messaging.Destination;
using BuildingBlocks.Messaging.TourJob;
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
            .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Days, opt =>
                opt.MapFrom(src => (src.EndDate - src.StartDate).Days));
        CreateMap<CreateTourJobCommand, TourDetail>();

        CreateMap<UpdateTourJobCommand, TourJob>()
            .BeforeMap<SlugResolver<UpdateTourJobCommand>>()
            .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Days, opt =>
                opt.MapFrom(src => (src.EndDate - src.StartDate).Days)); ;
        CreateMap<UpdateTourJobCommand, TourDetail>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<TourJobDto, TourJobCreated>();
        CreateMap<TourJobDto, TourJobUpdated>();
        #endregion

        #region Destination
        CreateMap<Destination, DestinationDto>().ReverseMap();

        CreateMap<CreateDestinationCommand, Destination>()
            .BeforeMap<SlugDestinationResolver<CreateDestinationCommand>>();

        CreateMap<UpdateDestinationCommand, Destination>()
            .BeforeMap<SlugDestinationResolver<UpdateDestinationCommand>>();

        CreateMap<DestinationDto, DestinationCreated>();
        CreateMap<DestinationDto, DestinationUpdated>();
        #endregion
    }
}
