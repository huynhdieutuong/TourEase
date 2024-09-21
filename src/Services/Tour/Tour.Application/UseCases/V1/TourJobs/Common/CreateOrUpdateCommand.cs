namespace Tour.Application.UseCases.V1.TourJobs;
public class CreateOrUpdateCommand
{
    public string Title { get; set; }
    public decimal SalaryPerDay { get; set; }
    public int Currency { get; set; }
    public DateTimeOffset ExpiredDate { get; set; }
    public string Itinerary { get; set; }
    public string? ImageUrl { get; set; }
    public int Participants { get; set; }
    public int LanguageSpoken { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<Guid> DestinationIds { get; set; }
}
