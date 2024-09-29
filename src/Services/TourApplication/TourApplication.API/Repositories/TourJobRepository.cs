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

        var sql = @"INSERT INTO TourJob (Id, ExpiredDate, Owner, IsFinished)
                    VALUES (@Id, @ExpiredDate, @Owner, @IsFinished)";

        var result = await connection.ExecuteAsync(
            sql,
            new { tourJob.Id, tourJob.ExpiredDate, tourJob.Owner, tourJob.IsFinished });

        return result > 0;
    }

    public async Task<bool> UpdateTourJobAsync(TourJob tourJob)
    {
        using var connection = _connectionFactory.Create();

        var sql = @"UPDATE TourJob
                    SET ExpiredDate = @ExpiredDate, @IsFinished = IsFinished
                    WHERE Id = @Id";

        var result = await connection.ExecuteAsync(
            sql,
            new { tourJob.Id, tourJob.ExpiredDate, tourJob.IsFinished });

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
}
