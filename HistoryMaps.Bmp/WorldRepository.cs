using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
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
        Directory.CreateDirectory(
            _rootFolder.GetPath("worlds" + Path.PathSeparator + world.Id));
        foreach (var country in world.Countries)
        {
            CreateImage(country).SaveAsBmp(
                _rootFolder.GetPath(
                    "worlds" + Path.PathSeparator + country.Id + "-" + country.Name));
        }
        CreateImage(world.Water).SaveAsBmp(
            _rootFolder.GetPath(
                "worlds" + Path.PathSeparator + "water"));
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

    private void Save(World world)
    {
    }

    private Image CreateImage(Area area)
    {
        var img = Image.Load(_rootFolder.GetPath("blank_image.bmp"));
        for (var x = 0; x < 361; x++)
        {
            for (var y = 0; y < 181; y++)
            {
                if (area.Points[x, y])
                {
                    var x1 = x;
                    var y1 = y;
                    img.Mutate(c => c.DrawLines(Color.Black, 1, new PointF(x1, y1), new PointF(x1 + 1, y1 + 1)));
                }
            }
        }
        return img;
    }
}