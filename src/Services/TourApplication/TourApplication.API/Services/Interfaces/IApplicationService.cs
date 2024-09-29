namespace TourApplication.API.Services.Interfaces;

public interface IApplicationService
{
    Task PublishTotalApplicantsUpdated(Guid tourJobId);
}
