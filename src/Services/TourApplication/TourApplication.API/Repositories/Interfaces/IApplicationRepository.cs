using TourApplication.API.Models;
using TourApplication.API.UseCases.V1;

namespace TourApplication.API.Repositories.Interfaces;

public interface IApplicationRepository
{
    Task<Application?> GetApplicationByIdAsync(Guid id);
    Task<Application?> GetApplicationByTourJobIdAndUsernameAsync(Guid tourJobId, string username);
    Task<List<Application>> GetApplicationsByTourJobIdAsync(Guid tourJobId);
    Task<List<Application>> GetMyApplications(string username);
    Task<Guid> CreateApplicationAsync(ApplyTourJobCommand command);
    Task<bool> CancelApplicationAsync(Guid applicationId);
    Task<bool> ReApplyApplicationAsync(Guid applicationId);
    Task ChooseTourGuideAsync(Guid applicationId, Guid tourJobId);

}
