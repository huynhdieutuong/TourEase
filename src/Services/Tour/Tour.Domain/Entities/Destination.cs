using BuildingBlocks.Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Tour.Domain.Entities.Enums;

namespace Tour.Domain.Entities;

[Index(nameof(Slug), IsUnique = true)]
public class Destination : EntityBase<Guid>
{
    public string Name { get; set; }
    public DestinationType Type { get; set; }
    public string? ImageUrl { get; set; }
    public string Slug { get; set; }

    public Guid? ParentId { get; set; }
    public Destination Parent { get; set; }

    public ICollection<Destination> SubDestinations { get; } = [];

    public ICollection<TourDetailDestination> TourDetailDestinations { get; set; } = [];
}
