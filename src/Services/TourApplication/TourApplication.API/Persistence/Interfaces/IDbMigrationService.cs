namespace TourApplication.API.Persistence.Interfaces;

public interface IDbMigrationService
{
    Task MigrateDatabaseAsync();
}
