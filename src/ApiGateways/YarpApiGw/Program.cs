using BuildingBlocks.Logging;
using BuildingBlocks.Shared.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog(Serilogger.Configure);

    Log.Information($"Starting {builder.Environment.ApplicationName} up");

    builder.Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

    builder.Services.AddIdentityService();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("customPolicy", b =>
        {
            b.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(builder.Configuration["ClientApp"]);
        });
    });

    var app = builder.Build();

    app.UseCors();

    app.MapReverseProxy();

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