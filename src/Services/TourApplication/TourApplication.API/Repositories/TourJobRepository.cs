using Dapper;
using TourApplication.API.Models;
using TourApplication.API.Persistence.Interfaces;
using TourApplication.API.Repositories.Interfaces;

namespace TourApplication.API.Repositories;

public class TourJobRepository : ITourJobRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public TourJobRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<TourJob?> GetTourJobByIdAsync(Guid id)
    {
        using var connection = _connectionFactory.Create();

        var sql = @"SELECT * FROM TourJob
                    WHERE Id = @Id";

        var result = await connection.QuerySingleOrDefaultAsync<TourJob>(
            sql,
            new { Id = id });

        return result;
    }

    public async Task<bool> SaveTourJobAsync(TourJob tourJob)
    {
        using var connection = _connectionFactory.Create();

        var sql = @"INSERT INTO TourJob (Id, ExpiredDate, Owner)
                    VALUES (@Id, @ExpiredDate, @Owner)";

        var result = await connection.ExecuteAsync(
            sql,
            new { tourJob.Id, tourJob.ExpiredDate, tourJob.Owner });

        return result > 0;
    }

    public async Task<bool> UpdateTourJobAsync(TourJob tourJob)
    {
        using var connection = _connectionFactory.Create();

        var sql = @"UPDATE TourJob
                    SET ExpiredDate = @ExpiredDate
                    WHERE Id = @Id";

        var result = await connection.ExecuteAsync(
            sql,
            new { tourJob.Id, tourJob.ExpiredDate });

        return result > 0;
    }

    public async Task<bool> DeleteTourJobAsync(Guid id)
    {
        using var connection = _connectionFactory.Create();

        var sql = @"DELETE FROM TourJob
                    WHERE Id = @Id";

        var result = await connection.ExecuteAsync(
            sql,
            new { Id = id });

        return result > 0;
    }

    public async Task<List<Guid>> GetExpiredTourJobIdsAsync()
    {
        using var connection = _connectionFactory.Create();

        var sql = @"SELECT Id FROM TourJob
                    WHERE ExpiredDate <= @Now AND IsFinished = 0";

        var result = await connection.QueryAsync<Guid>(
            sql,
            new { Now = DateTime.UtcNow });

        return result.ToList();
    }

    public async Task<int> SetExpiredTourJobsToFinishedAsync(List<Guid> tourJobIds)
    {
        using var connection = _connectionFactory.Create();

        var sqlTourJobFinished = @"
                UPDATE TourJob
                SET IsFinished = 1
                WHERE Id IN @TourJobIds";

        var result = await connection.ExecuteAsync(
            sqlTourJobFinished,
            new { TourJobIds = tourJobIds });

        return result;
    }
}
