namespace TourApplication.API.DTOs;

public class TourJobDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public DateTime ExpiredDate { get; set; }
    public string Owner { get; set; }
    public bool IsFinished { get; set; }
}
