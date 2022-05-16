using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Processing;

namespace HistoryMaps;

public class WorldRepository : IWorldRepository
{
    private readonly IRootFolderProvider _rootFolder;

    public WorldRepository(IRootFolderProvider rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public void Insert(World world)
    {
        var img = Image.Load(new byte[0]);
        img.Mutate(x =>x.FillPolygon(Color.Gold, new PointF(1, 1)));
        img.Save("t.bmp", new BmpEncoder{BitsPerPixel = BmpBitsPerPixel.Pixel1});

    }

    public void Update(World world)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid worldId)
    {
        throw new NotImplementedException();
    }

    public World Get(Guid worldId)
    {
        throw new NotImplementedException();
    }
}