using BuildingBlocks.Logging;
using BuildingBlocks.Shared.Middlewares;
using Serilog;
using TourApplication.API.Extensions;
using TourApplication.API.Persistence.Interfaces;

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

    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseMiddleware<ErrorWrappingMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var migrationService = scope.ServiceProvider.GetRequiredService<IDbMigrationService>();
        await migrationService.MigrateDatabaseAsync();
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