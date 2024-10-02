using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Messaging.TourJob;
using TourApplication.API.DTOs;
using TourApplication.API.Models;

namespace TourApplication.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Application, ApplicationDto>();
        CreateMap<TourJob, TourJobDto>();
        CreateMap<ApplicationWithTourJob, ApplicationDto>()
            .IncludeMembers(x => x.Application);

        CreateMap<Application, ApplicationMessage>();
        CreateMap<TourJob, TourJobMessage>();

        CreateMap<TourJobCreated, TourJob>()
            .ForMember(des => des.ExpiredDate, opt => opt.MapFrom(src => src.ExpiredDate.DateTime))
            .ForMember(des => des.Owner, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(des => des.IsFinished, opt => opt.MapFrom(src => src.ExpiredDate <= DateTimeOffset.UtcNow));

        CreateMap<TourJobUpdated, TourJob>()
            .ForMember(des => des.ExpiredDate, opt => opt.MapFrom(src => src.ExpiredDate.DateTime))
            .ForMember(des => des.Owner, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(des => des.IsFinished, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.TourGuide) || src.ExpiredDate <= DateTimeOffset.UtcNow));
    }
}
