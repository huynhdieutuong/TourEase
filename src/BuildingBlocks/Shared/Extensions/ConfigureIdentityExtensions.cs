using BuildingBlocks.Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Shared.Extensions;
public static class ConfigureIdentityExtensions
{
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
