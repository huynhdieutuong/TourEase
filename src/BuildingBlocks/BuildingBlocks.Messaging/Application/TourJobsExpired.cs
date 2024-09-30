using BuildingBlocks.Contracts.Domains;

namespace BuildingBlocks.Messaging.Application;
public class TourJobsExpired : IntegrationEventBase
{
    public List<Guid> TourJobIds { get; set; }
}
