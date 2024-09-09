using BuildingBlocks.Shared.Constants;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        AddRoles(scope);
        AddUsers(scope);
    }

    private static void AddRoles(IServiceScope scope)
    {
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (roleMgr.Roles.Any()) return;

        var admin = roleMgr.FindByNameAsync(Roles.Admin).Result;
        if (admin == null)
        {
            admin = new IdentityRole(Roles.Admin);
            var result = roleMgr.CreateAsync(admin).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("Admin role created");
        }

        var travelAgency = roleMgr.FindByNameAsync(Roles.TravelAgency).Result;
        if (travelAgency == null)
        {
            travelAgency = new IdentityRole(Roles.TravelAgency);
            var result = roleMgr.CreateAsync(travelAgency).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("TravelAgency role created");
        }

        var tourGuide = roleMgr.FindByNameAsync(Roles.TourGuide).Result;
        if (tourGuide == null)
        {
            tourGuide = new IdentityRole(Roles.TourGuide);
            var result = roleMgr.CreateAsync(tourGuide).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("TourGuide role created");
        }
    }

    private static void AddUsers(IServiceScope scope)
    {
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (userMgr.Users.Any()) return;

        var tuongAdmin = userMgr.FindByNameAsync("tuongadmin").Result;
        if (tuongAdmin == null)
        {
            tuongAdmin = new ApplicationUser
            {
                UserName = "tuongadmin",
                Email = "TuongAdmin@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(tuongAdmin, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddToRolesAsync(tuongAdmin, [Roles.Admin, Roles.TravelAgency, Roles.TourGuide]).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("Tuong Admin user created");
        }

        var tuongAgency = userMgr.FindByNameAsync("tuongagency").Result;
        if (tuongAgency == null)
        {
            tuongAgency = new ApplicationUser
            {
                UserName = "tuongagency",
                Email = "TuongAgency@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(tuongAgency, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddToRoleAsync(tuongAgency, Roles.TravelAgency).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("Tuong Agency user created");
        }

        var tuongGuide = userMgr.FindByNameAsync("tuongguide").Result;
        if (tuongGuide == null)
        {
            tuongGuide = new ApplicationUser
            {
                UserName = "tuongguide",
                Email = "TuongGuide@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(tuongGuide, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddToRoleAsync(tuongGuide, Roles.TourGuide).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("Tuong Guide user created");
        }
    }
}
