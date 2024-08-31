using MongoDB.Driver;

namespace BuildingBlocks.Shared.Paging;
public class PagedList<T> : List<T>
{
    private MetaData _metaData { get; }

    public MetaData GetMetaData() => _metaData;

    public PagedList(IEnumerable<T> items, long totalItems, int pageIndex, int pageSize)
    {
        _metaData = new MetaData
        {
            TotalItems = totalItems,
            PageSize = pageSize,
            CurrentPage = pageIndex,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
        };
        AddRange(items);
    }

    public static async Task<PagedList<T>> ToPagedList(IMongoCollection<T> source,
                                                       FilterDefinition<T> filter,
                                                       SortDefinition<T> sort,
                                                       int pageIndex,
                                                       int pageSize)
    {
        var count = await source.Find(filter).CountDocumentsAsync();
        var items = await source.Find(filter)
                                .Sort(sort)
                                .Skip((pageIndex - 1) * pageSize)
                                .Limit(pageSize)
                                .ToListAsync();

        return new PagedList<T>(items, count, pageIndex, pageSize);
    }
}
