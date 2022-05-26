using System.Data;
namespace HistoryMaps;

public interface IConnectionFactory
{
    public IDbConnection CreateConnection();
}