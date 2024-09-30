using BuildingBlocks.Shared.Exceptions;
using TourApplication.API.Models;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.Services.Interfaces;
using ILogger = Serilog.ILogger;

namespace TourApplication.API.Services;

public class TourJobService : ITourJobService
{
    private readonly ILogger _logger;
    private readonly IGrpcTourJobClient _rpcTourJobClient;
    private readonly ITourJobRepository _tourJobRepository;

    public TourJobService(ILogger logger,
                          IGrpcTourJobClient rpcTourJobClient,
                          ITourJobRepository tourJobRepository)
    {
        _logger = logger;
        _rpcTourJobClient = rpcTourJobClient;
        _tourJobRepository = tourJobRepository;
    }

    public async Task<TourJob> GetOrSaveTourJobAsync(Guid id)
    {
        var tourJob = await _tourJobRepository.GetTourJobByIdAsync(id);
        if (tourJob == null)
        {
            _logger.Warning($"Tour job not found for ID: {id}. Fetching from Tour Service...");

            tourJob = _rpcTourJobClient.GetTourJob(id.ToString());
            if (tourJob == null) throw new BadRequestException("Something went wrong.");

            await _tourJobRepository.SaveTourJobAsync(tourJob);
        }

        return tourJob;
    }

    public void CheckValidTourJob(TourJob tourJob)
    {
        if (tourJob.IsFinished || tourJob.ExpiredDate <= DateTime.UtcNow)
        {
            throw new BadRequestException("The tour job has expired or finished.");
        }
    }
}
