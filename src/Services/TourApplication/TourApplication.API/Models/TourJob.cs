using BuildingBlocks.Contracts.Domains;

namespace TourApplication.API.Models;

public class TourJob : EntityBase<Guid>
{
    public DateTime ExpiredDate { get; set; } // update TourJob, need sync to update (if not exist, insert)
    public string Owner { get; set; } // to check Owner can't Apply their job
    public bool IsFinished { get; set; } // to get list Expired TourJobs in background (exclude Finish job, although ExpiredDate < Now)
}
