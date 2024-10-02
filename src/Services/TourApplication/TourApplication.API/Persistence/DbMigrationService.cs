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

            _logger.LogInformation("Adding new columns to TourJob table.");
            await AddNewColumnsToTourJobTableAsync(connection);

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

    private async Task AddNewColumnsToTourJobTableAsync(IDbConnection connection)
    {
        // Add Title column if it doesn't exist
        await AddColumnIfNotExistsAsync(connection, "TourJob", "Title", "NVARCHAR(255) NOT NULL");

        // Add Slug column if it doesn't exist
        await AddColumnIfNotExistsAsync(connection, "TourJob", "Slug", "NVARCHAR(255) NOT NULL");
    }

    private async Task AddColumnIfNotExistsAsync(IDbConnection connection, string tableName, string columnName, string columnDefinition)
    {
        var checkColumnExistsQuery = @"
            SELECT COUNT(*)
            FROM information_schema.COLUMNS
            WHERE TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName;
        ";

        var columnExists = await connection.ExecuteScalarAsync<int>(checkColumnExistsQuery, new { TableName = tableName, ColumnName = columnName });

        if (columnExists == 0)
        {
            var addColumnQuery = $@"
                ALTER TABLE {tableName}
                ADD COLUMN {columnName} {columnDefinition};
            ";

            await connection.ExecuteAsync(addColumnQuery);
            _logger.LogInformation($"{columnName} column added to {tableName} table.");
        }
        else
        {
            _logger.LogInformation($"{columnName} column already exists in {tableName} table.");
        }
    }
}
