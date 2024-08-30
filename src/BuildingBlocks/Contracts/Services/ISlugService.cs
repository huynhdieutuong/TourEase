namespace BuildingBlocks.Contracts.Services;
public interface ISlugService
{
    string GenerateTourJobSlug(string title);
    string GenerateDestinationSlug(string title);
    string GenerateSlug(string text);
}
