using BuildingBlocks.Logging;
using Serilog;
using TourSearch.API.Extensions;
using TourSearch.API.Persistence;

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

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices();
    builder.Services.ConfigureHttpClients(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseAuthorization();

    app.MapControllers();

    // Initialize and Seed database
    using (var scope = app.Services.CreateScope())
    {
        var tourSearchSeed = scope.ServiceProvider.GetRequiredService<TourSearchSeed>();
        await tourSearchSeed.SeedDataAsync();
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