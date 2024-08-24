namespace Tour.Application.UseCases.V1.Destinations;
public class CreateOrUpdateDestinationCommand
{
    public string Name { get; set; }
    public int Type { get; set; }
    public string? ImageUrl { get; set; }
    public Guid? ParentId { get; set; }
}
