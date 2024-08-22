using BuildingBlocks.Contracts.Services;
using Slugify;

namespace BuildingBlocks.Infrastructure.Services;
public class SlugService : ISlugService
{
    private readonly ISlugHelper _slugHelper;

    public SlugService()
    {
        _slugHelper = new SlugHelper();
    }

    public string GenerateTourJobSlug(string title)
    {
        var baseSlug = _slugHelper.GenerateSlug(title);
        var suffix = Guid.NewGuid().ToString("N").Substring(0, 8);
        return $"{baseSlug}-{suffix}";
    }

    public string GenerateDestinationSlug(string title)
    {
        return _slugHelper.GenerateSlug(title);
    }
}
