using System.Drawing;

namespace HistoryMaps;

public interface IWorldBmpRepository
{
    void Insert(World world);
    void Update(World world);
    void Delete(Guid worldId);
    World Get(Guid worldId);
}