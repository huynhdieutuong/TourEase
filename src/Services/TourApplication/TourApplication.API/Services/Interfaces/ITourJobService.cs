using TourApplication.API.Models;

namespace TourApplication.API.Services.Interfaces;

public interface ITourJobService
{
    Task<TourJob> GetOrSaveTourJobAsync(Guid id);
    void CheckValidTourJob(TourJob job);
}
