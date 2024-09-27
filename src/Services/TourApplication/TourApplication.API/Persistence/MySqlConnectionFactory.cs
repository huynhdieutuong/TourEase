using MySql.Data.MySqlClient;
using System.Data;
using TourApplication.API.Persistence.Interfaces;

namespace TourApplication.API.Persistence;

public class MySqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MySqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection Create()
    {
        var connection = new MySqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
