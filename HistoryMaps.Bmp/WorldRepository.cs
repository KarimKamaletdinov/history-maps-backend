using System.Drawing;

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
        var path = _rootFolder.GetPath("worlds" + Path.DirectorySeparatorChar + world.Id);
        if (File.Exists(path))
            throw new AlreadyExistsException($"File \"{path}\" already exists!");

        using var image = (Bitmap)Image.FromFile(_rootFolder.GetPath(
            "constants" + Path.DirectorySeparatorChar + "base_image.bmp"));
        
        for (var x = 0; x < 361; x++)
        {
            for (var y = 0; y < 181; y++)
            {
                if (world.Water.Points[y, x])
                    image.SetPixel(x, y, world.Water.Color);
                else
                {
                    foreach (var country in world.Countries)
                    {
                        if (country.Points[y, x])
                            image.SetPixel(x, y, country.Color);
                    }
                }
            }
        }

        image.Save(path);
    }

    public void Update(World world)
    {
        var path = "worlds" + Path.PathSeparator + world.Id;
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");

        using var image = (Bitmap)Image.FromFile("constants" + Path.PathSeparator + "base_image.bmp");
        
        for (var x = 0; x < 361; x++)
        {
            for (var y = 0; y < 181; y++)
            {
                if (world.Water.Points[x, y])
                    image.SetPixel(x, y, world.Water.Color);
                else
                {
                    foreach (var country in world.Countries)
                    {
                        if (country.Points[x, y])
                            image.SetPixel(x, y, country.Color);
                    }
                }
            }
        }

        image.Save(path);
    }

    public void Delete(Guid worldId)
    {
        var path = "worlds" + Path.PathSeparator + worldId;
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");
        File.Delete(path);
    }

    public World Get(Guid worldId, Dictionary<string, Color> colorDictionary)
    {
        var path = "worlds" + Path.PathSeparator + worldId;
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");

        using var image = (Bitmap)Image.FromFile("constants" + Path.PathSeparator + "base_image.bmp");
        
        var water = new Area("water", colorDictionary["water"]);
        var countries = new List<Area>();

        for (var x = 0; x < 361; x++)
        {
            for (var y = 0; y < 181; y++)
            {
                foreach (var (name, color) in colorDictionary)
                {
                    if (color == image.GetPixel(x, y))
                    {
                        if (name == "water")
                            water.Points[x, y] = true;
                        else
                        {
                            if (countries.Any(x => x.Color == color))
                                countries.Find(x => x.Color == color).Points[x, y] = true;
                            else
                            {
                                var country = new Area(name, color);
                                country.Points[x, y] = true;
                                countries.Add(country);
                            }
                        }
                    }
                }
            }
        }

        return new World(worldId, water, countries);
    }
}