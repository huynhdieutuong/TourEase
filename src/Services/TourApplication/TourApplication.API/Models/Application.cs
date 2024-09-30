using BuildingBlocks.Contracts.Domains;

namespace TourApplication.API.Models;

public class Application : EntityBase<Guid>
{
    public Guid TourJobId { get; set; }
    public string TourGuide { get; set; }
    public string Comment { get; set; }
    public DateTime AppliedDate { get; set; }
    public Status Status { get; set; }
}
