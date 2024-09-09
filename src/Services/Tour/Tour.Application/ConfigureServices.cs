using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Infrastructure.Services;
using BuildingBlocks.Shared.Behaviors;
using BuildingBlocks.Shared.Configurations;
using BuildingBlocks.Shared.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tour.Application.Services;
using Tour.Application.Services.Interfaces;

namespace Tour.Application;
public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<ISlugService, SlugService>();
        services.AddScoped<IDestinationService, DestinationService>();
    }

    public static void AddIdentityService(this IServiceCollection services)
    {
        var settings = services.GetOptions<IdentitySettings>(nameof(IdentitySettings));
        if (settings == null || string.IsNullOrEmpty(settings.IdentityServiceUrl))
            throw new ArgumentNullException($"{nameof(IdentitySettings)} is not configured properly");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = settings.IdentityServiceUrl;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.NameClaimType = "username";
            });
    }
}
