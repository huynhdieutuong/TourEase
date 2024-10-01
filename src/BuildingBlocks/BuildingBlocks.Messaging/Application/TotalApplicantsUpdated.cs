using BuildingBlocks.Contracts.Domains;
using BuildingBlocks.Messaging.Enums;

namespace BuildingBlocks.Messaging.Application;
public class TotalApplicantsUpdated : IntegrationEventBase
{
    public Guid TourJobId { get; set; }
    public int TotalApplicants { get; set; }
    public ApplicationTypes Type { get; set; }
}
