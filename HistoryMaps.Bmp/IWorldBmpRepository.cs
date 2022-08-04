using System.Drawing;

namespace HistoryMaps;

public interface IWorldBmpRepository
{
    void Insert(World world);
    void InsertBitmap(Guid id, WorldBitmapDto world);
    void Update(World world);
    void Delete(Guid worldId);
    void ClearAll();
    IEnumerable<Guid> GetAllIds();
    World GetBaseWorld();
    World Get(Guid worldId);
    WorldBitmapDto GetBitmap(Guid id);
}