using BuildingBlocks.Contracts.Domains;
using Tour.Domain.Entities.Enums;

namespace Tour.Domain.Entities;
public class TourDetail : EntityBase<Guid>
{
    public string Itinerary { get; set; }
    public string? ImageUrl { get; set; }
    public int Participants { get; set; }
    public Language LanguageSpoken { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public Guid TourJobId { get; set; }
    public TourJob TourJob { get; set; }

    public ICollection<TourDetailDestination> TourDetailDestinations { get; set; } = [];
}
