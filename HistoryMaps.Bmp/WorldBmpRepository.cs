using System.Drawing;

namespace HistoryMaps;

public class WorldBmpRepository : IWorldBmpRepository
{
    private readonly IRootFolderProvider _rootFolder;

    public WorldBmpRepository(IRootFolderProvider rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public void Insert(World world)
    {
        var path = GenPath(world.Id);

        if (File.Exists(path))
            throw new AlreadyExistsException($"File \"{path}\" already exists!");

        using var image = new Bitmap(1080, 541);
        
        for (var x = 0; x < 1080; x++)
        {
            for (var y = 0; y < 541; y++)
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
        var path = GenPath(world.Id);
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");

        using var image = new Bitmap(1080, 541);
        
        for (var x = 0; x < 1080; x++)
        {
            for (var y = 0; y < 541; y++)
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
        var path = GenPath(worldId);
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");
        File.Delete(path);
    }

    public World Get(Guid worldId, Dictionary<string, Color> colorDictionary)
    {
        var path = GenPath(worldId);
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");
        
        using var image = (Bitmap)Image.FromFile(path);
        
        var water = new Area(colorDictionary["water"]);
        var countries = new List<Country>();
        var p = image.GetPixel(541, 180);
        for (var x = 0; x < 1080; x++)
        {
            for (var y = 0; y < 541; y++)
            {
                var pixel = image.GetPixel(x, y);
                foreach (var (name, color) in colorDictionary)
                {
                    if (color.R == pixel.R && color.G == pixel.G && color.B == pixel.B)
                    {
                        if (name == "water")
                            water.Points[y, x] = true;
                        else
                        {
                            if (countries.Any(c => c.Color == color))
                                countries.Find(c => c.Color == color)!.Points[y, x] = true;
                            else
                            {
                                var country = new Country(name, color);
                                country.Points[y, x] = true;
                                countries.Add(country);
                            }
                        }
                    }
                }
            }
        }

        return new World(worldId, water, countries);
    }

    private string GenPath(Guid worldId)
    {
        return _rootFolder.GetPath("worlds" + Path.DirectorySeparatorChar + worldId + ".bmp");
    }
}