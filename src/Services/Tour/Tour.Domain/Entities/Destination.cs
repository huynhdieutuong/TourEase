using BuildingBlocks.Contracts.Domains;
using Tour.Domain.Entities.Enums;

namespace Tour.Domain.Entities;
public class Destination : EntityBase<Guid>
{
    public string Name { get; set; }
    public DestinationType Type { get; set; }

    public Guid? ParentId { get; set; }
    public Destination Parent { get; set; }

    public ICollection<Destination> SubDestinations { get; } = [];

    public ICollection<TourDetailDestination> TourDetailDestinations { get; set; } = [];
}
