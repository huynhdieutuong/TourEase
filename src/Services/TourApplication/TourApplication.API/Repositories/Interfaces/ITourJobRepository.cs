using TourApplication.API.Models;

namespace TourApplication.API.Repositories.Interfaces;

public interface ITourJobRepository
{
    Task<TourJob?> GetTourJobByIdAsync(Guid id);
    Task<bool> SaveTourJobAsync(TourJob tourJob);
    Task<bool> UpdateTourJobAsync(TourJob tourJob);
    Task<bool> DeleteTourJobAsync(Guid id);
    Task<List<Guid>> GetExpiredTourJobIdsAsync();
    Task<int> SetExpiredTourJobsToFinishedAsync(List<Guid> tourJobIds);
}
