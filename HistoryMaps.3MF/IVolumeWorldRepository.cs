namespace HistoryMaps;

public interface IVolumeWorldRepository
{
    void Insert(WorldDto world);
    void ClearAll();
    IEnumerable<Guid> GetAllIds();
}