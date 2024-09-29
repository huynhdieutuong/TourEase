using TourApplication.API.Models;

namespace TourApplication.API.Repositories.Interfaces;

public interface ITourJobRepository
{
    Task<TourJob?> GetTourJobByIdAsync(Guid id);
    Task<bool> SaveTourJobAsync(TourJob tourJob);
}
