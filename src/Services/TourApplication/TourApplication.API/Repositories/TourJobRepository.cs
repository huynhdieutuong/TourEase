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
}
