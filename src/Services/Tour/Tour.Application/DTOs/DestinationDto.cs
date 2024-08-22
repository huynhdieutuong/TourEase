namespace Tour.Application.DTOs;
public class DestinationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string? ImageUrl { get; set; }
    public string Slug { get; set; }
}
