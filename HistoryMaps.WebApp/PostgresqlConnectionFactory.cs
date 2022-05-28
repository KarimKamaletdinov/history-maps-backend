using System.Data;
using Npgsql;

namespace HistoryMaps;

public class PostgresqlConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;

    public PostgresqlConnectionFactory()
    {
        throw new NotImplementedException();
    }

    public PostgresqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}