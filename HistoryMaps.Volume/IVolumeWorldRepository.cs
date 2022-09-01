namespace HistoryMaps;

public interface IVolumeWorldRepository
{
    void Insert(WorldDto world);
    void InsertBaseWorld(WorldDto world);
    void ClearAll();
    IEnumerable<Guid> GetAllIds();
    bool BaseWorldExists();
}