using BuildingBlocks.Contracts.Domains;

namespace TourApplication.API.Models;

public class TourJob : EntityBase<Guid>
{
    public DateTime ExpiredDate { get; set; }
    public string Owner { get; set; }
    public bool IsFinished { get; set; }
}
