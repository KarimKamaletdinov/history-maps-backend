namespace HistoryMaps.Persistence;

public interface IStlRepository
{
    void Insert(long id);
    StlDocument GetStl(long id);
    byte[] GetBytes(long id);
}