namespace BuildingBlocks.Messaging.Application;
public class ApplicationMessage
{
    public Guid Id { get; set; }
    public Guid TourJobId { get; set; }
    public string TourGuide { get; set; }
    public string Comment { get; set; }
    public DateTime AppliedDate { get; set; }
    public string Status { get; set; }
}

