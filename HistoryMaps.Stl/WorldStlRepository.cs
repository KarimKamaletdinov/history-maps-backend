using System.Drawing;

namespace HistoryMaps;

class WorldStlRepository : IWorldStlRepository
{
    private readonly IRootFolderProvider _rootFolder;

    public WorldStlRepository(IRootFolderProvider rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public void Insert(World world)
    {
        throw new NotImplementedException();
    }

    public void Update(World world)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid worldId)
    {
        throw new NotImplementedException();
    }

    public World Get(Guid worldId, Dictionary<string, Color> colorDictionary)
    {
        throw new NotImplementedException();
    }

    private string GenPath(Guid worldId)
    {
        return _rootFolder.GetPath("worlds" + Path.DirectorySeparatorChar + worldId + ".stl");
    }
}