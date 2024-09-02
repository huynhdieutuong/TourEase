using BuildingBlocks.Contracts.Common;
using BuildingBlocks.Contracts.Domains;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BuildingBlocks.Infrastructure.Common;

public abstract class MongoRepositoryBase<T, K> : IMongoRepositoryBase<T, K> where T : MongoEntityBase<K>
{
    protected readonly IMongoCollection<T> _collection;

    public MongoRepositoryBase(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    #region Query
    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        return await _collection.Find(filter ?? Builders<T>.Filter.Empty).ToListAsync();
    }

    public async Task<T> FindSingleAsync(Expression<Func<T, bool>> filter)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> FindByIdAsync(K id)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    #endregion

    #region Command
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

    public async Task DeleteByIdAsync(K id)
    {
        var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
    {
        await _collection.DeleteManyAsync(filter);
    }
    #endregion
}