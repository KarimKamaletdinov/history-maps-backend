using System.Drawing;

namespace HistoryMaps;

public interface IWorldBmpRepository
{
    Task Insert(World world);
    Task InsertBitmap(Guid id, WorldBitmapDto world);
    Task ClearAll();
    Task<IEnumerable<Guid>> GetAllIds();
    Task<World> GetBaseWorld();
    Task<World> Get(Guid worldId);
    Task<WorldBitmapDto> GetBitmap(Guid id);
}