namespace HistoryMaps;

public interface IWorld3MfRepository
{
    void Insert(WorldDto world);
    void InsertSeparately(WorldDto world);
    void ClearAll();
}