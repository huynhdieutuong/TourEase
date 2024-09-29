using Grpc.Net.Client;
using Tour.Application;
using TourApplication.API.Models;
using TourApplication.API.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.Services;

public class GrpcTourJobClient : IGrpcTourJobClient
{
    private readonly ILogger _logger;
    private readonly IConfiguration _config;

    public GrpcTourJobClient(ILogger logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public TourJob GetTourJob(string id)
    {
        _logger.Information("Calling GRPC Service");
        var channel = GrpcChannel.ForAddress(_config["GrpcTourJob"]);
        var client = new GrpcTourJob.GrpcTourJobClient(channel);
        var request = new GetTourJobRequest { Id = id };

        try
        {
            var reply = client.GetTourJob(request);
            var tourJob = new TourJob
            {
                Id = Guid.Parse(reply.Tourjob.Id),
                ExpiredDate = DateTime.Parse(reply.Tourjob.ExpiredDate),
                Owner = reply.Tourjob.Owner,
                IsFinished = DateTime.Parse(reply.Tourjob.ExpiredDate) < DateTime.UtcNow
            };
            return tourJob;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Could not call GRPC Server");
            return null;
        }
    }
}
