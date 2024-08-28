using BuildingBlocks.Contracts.Domains;

namespace Tour.Domain.Entities;
public class TourDetailDestination : EntityBase<Guid>
{
    public Guid TourDetailId { get; set; }
    public TourDetail TourDetail { get; set; }

    public Guid DestinationId { get; set; }
    public Destination Destination { get; set; }
}
