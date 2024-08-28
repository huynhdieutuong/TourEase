using Tour.Application.Services.Interfaces;
using Tour.Domain.Entities;

namespace Tour.Application.Services;
public class DestinationService : IDestinationService
{
    public List<Destination> BuildTree(List<Destination> destinations, Guid? parrentId = null)
    {
        var lookup = destinations.ToLookup(d => d.ParentId);
        return BuildTreeRecursive(lookup, parrentId);
    }

    private List<Destination> BuildTreeRecursive(ILookup<Guid?, Destination> lookup, Guid? parrentId)
    {
        var children = lookup[parrentId].ToList();
        if (children.Count == 0) return [];

        foreach (var child in children)
        {
            child.SubDestinations = BuildTreeRecursive(lookup, child.Id);
        }
        return children;
    }
}
