using BuildingBlocks.Logging;
using Serilog;
using SignalR.Extensions;
using SignalR.Hubs;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog(Serilogger.Configure);

    Log.Information($"Starting {builder.Environment.ApplicationName} up");

    // Add services to the container.

    builder.Services.AddApplicationServices();

    builder.Services.AddSignalR();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.MapHub<NotificationHub>("/notifications");

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
