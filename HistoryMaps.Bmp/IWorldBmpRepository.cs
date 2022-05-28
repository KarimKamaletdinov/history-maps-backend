using System.Drawing;

namespace HistoryMaps;

public interface IWorldBmpRepository
{
    void Insert(World world);
    void Update(World world);
    void Delete(Guid worldId);
    void ClearAll();
    World GetBaseWorld();
    World Get(Guid worldId);
}