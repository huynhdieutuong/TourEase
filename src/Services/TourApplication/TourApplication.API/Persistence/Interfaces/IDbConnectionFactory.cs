using System.Data;

namespace TourApplication.API.Persistence.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}
