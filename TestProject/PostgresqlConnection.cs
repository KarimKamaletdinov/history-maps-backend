using System.Data;
using Npgsql;

namespace HistoryMaps;

public class PostgresqlConnectionFactory : IConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection("Host=localhost;Username=history_maps;Password=163264;Database=history_maps");
    }
}