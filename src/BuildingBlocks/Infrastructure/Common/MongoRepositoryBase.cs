using BuildingBlocks.Contracts.Common;
using BuildingBlocks.Contracts.Domains;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BuildingBlocks.Infrastructure.Common;

public class MongoRepositoryBase<T> : IMongoRepositoryBase<T> where T : MongoEntityBase
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepositoryBase(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        return await _collection.Find(filter ?? Builders<T>.Filter.Empty).ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> GetBySlugAsync(string slug)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Slug, slug);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task InsertManyAsync(IEnumerable<T> entities)
    {
        await _collection.InsertManyAsync(entities);
    }

    public async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
    {
        await _collection.DeleteManyAsync(filter);
    }
}