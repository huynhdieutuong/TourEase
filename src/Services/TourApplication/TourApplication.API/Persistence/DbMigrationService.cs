using Dapper;
using System.Data;
using TourApplication.API.Persistence.Interfaces;

namespace TourApplication.API.Persistence;

public class DbMigrationService : IDbMigrationService
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ILogger<DbMigrationService> _logger;

    public DbMigrationService(IDbConnectionFactory dbConnectionFactory,
                              ILogger<DbMigrationService> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _logger = logger;
    }

    public async Task MigrateDatabaseAsync()
    {
        _logger.LogInformation("Database migration started.");
        try
        {
            using var connection = _dbConnectionFactory.Create();

            _logger.LogInformation("Creating Application table.");
            await CreateApplicationTableAsync(connection);

            _logger.LogInformation("Creating TourJob table.");
            await CreateTourJobTableAsync(connection);

            _logger.LogInformation("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }

    private async Task CreateApplicationTableAsync(IDbConnection connection)
    {
        var createApplicationTableQuery = @"
            CREATE TABLE IF NOT EXISTS Application (
                Id CHAR(36) PRIMARY KEY,
                TourJobId CHAR(36) NOT NULL,
                TourGuide VARCHAR(255) NOT NULL,
                Comment VARCHAR(500) NOT NULL,
                AppliedDate DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                Status INT NOT NULL DEFAULT 0
            );
        ";

        await connection.ExecuteAsync(createApplicationTableQuery);
        _logger.LogInformation("Application table created or already exists.");
    }

    private async Task CreateTourJobTableAsync(IDbConnection connection)
    {
        var createTourJobTableQuery = @"
            CREATE TABLE IF NOT EXISTS TourJob (
                Id CHAR(36) PRIMARY KEY,
                ExpiredDate DATETIME(6) NOT NULL,
                Owner VARCHAR(255) NOT NULL,
                IsFinished BOOLEAN NOT NULL DEFAULT FALSE
            );
        ";

        await connection.ExecuteAsync(createTourJobTableQuery);
        _logger.LogInformation("TourJob table created or already exists.");
    }
}
