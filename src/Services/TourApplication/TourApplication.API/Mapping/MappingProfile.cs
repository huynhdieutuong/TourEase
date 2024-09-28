using AutoMapper;
using TourApplication.API.DTOs;
using TourApplication.API.Models;

namespace TourApplication.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Application, ApplicationDto>();
    }
}
