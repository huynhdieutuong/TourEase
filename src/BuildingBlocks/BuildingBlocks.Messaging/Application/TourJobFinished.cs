using BuildingBlocks.Contracts.Domains;

namespace BuildingBlocks.Messaging.Application;
public class TourJobFinished : IntegrationEventBase
{
    public Guid TourJobId { get; set; }
    public string TourGuide { get; set; }
}
