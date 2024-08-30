using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Infrastructure.Services;
using TourSearch.API.Services;
using TourSearch.API.Services.Interfaces;

namespace TourSearch.API.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISerializerService, SerializerService>();

        services.AddScoped<ITourJobService, TourJobService>();
        services.AddScoped<IDestinationService, DestinationService>();
    }
}
