using BuildingBlocks.Logging;
using BuildingBlocks.Shared.Extensions;
using Serilog;
using Tour.Application;
using Tour.Infrastructure;
using Tour.Infrastructure.Persistence;
using Tour.Infrastructure.Redis;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog(Serilogger.Configure);

    Log.Information($"Starting {builder.Environment.ApplicationName} up");

    // Add services to the container.
    builder.Services.AddControllers();
    //builder.Services.AddCarter();

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices();
    builder.Services.AddInfrastructureRedisServices();
    builder.Services.AddIdentityService();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    //app.MapCarter();

    // Initialize and Seed database
    using (var scope = app.Services.CreateScope())
    {
        var tourSeed = scope.ServiceProvider.GetRequiredService<TourSeed>();
        await tourSeed.InitializeAsync();
        await tourSeed.SeedAsync();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
