using BuildingBlocks.Contracts.Domains;

namespace BuildingBlocks.Messaging.Application;
public class TotalApplicantsUpdated : IntegrationEventBase
{
    public Guid TourJobId { get; set; }
    public int TotalApplicants { get; set; }
}
