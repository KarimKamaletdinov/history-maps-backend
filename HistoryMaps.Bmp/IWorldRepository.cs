using System.Drawing;

namespace HistoryMaps;

public interface IWorldRepository
{
    void Insert(World world);
    void Update(World world);
    void Delete(Guid worldId);
    World Get(Guid worldId, Dictionary<string, Color> colorDictionary);
}