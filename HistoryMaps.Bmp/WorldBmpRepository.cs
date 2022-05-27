using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        using var image = new Bitmap(Map.Width, Map.Height);
        
        for (var x = 0; x < Map.Width; x++)
        {
            for (var y = 0; y < Map.Height; y++)
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

    public void Update(World world)
    {
        var path = GenPath(world.Id);
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");

        using var image = new Bitmap(Map.Width, Map.Height);
        
        for (var x = 0; x < Map.Width; x++)
        {
            for (var y = 0; y < Map.Height; y++)
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

    public World GetBaseWorld() => Get(Guid.Empty);

    public World Get(Guid worldId)
    {
        var colorDictionary = GetColors(worldId);
        var path = GenPath(worldId);
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");
        
        using var image = (Bitmap)Image.FromFile(path);
        
        var water = new MapArea(new bool[Map.Width, Map.Height], colorDictionary["water"]);
        var countries = new List<Country>();
        var p = image.GetPixel(Map.Height, 180);
        for (var x = 0; x < Map.Width; x++)
        {
            for (var y = 0; y < Map.Height; y++)
            {
                var pixel = image.GetPixel(x, y);
                foreach (var (name, color) in colorDictionary)
                {
                    if (color.R == pixel.R && color.G == pixel.G && color.B == pixel.B)
                    {
                        if (name == "water")
                            water.Points[x, y] = true;
                        else
                        {
                            if (countries.Any(c => c.Color == color))
                                countries.Find(c => c.Color == color)!.Points[x, y] = true;
                            else
                            {
                                var country = new Country(new bool[Map.Width, Map.Height], name, color);
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

    private Dictionary<string, Color> GetColors(Guid worldId)
    {
        var json = File.ReadAllText(_rootFolder.GetPath("worlds", worldId + ".mtd"));
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, Rgb>>(json) ?? 
                          throw new DomainException("Invalid colors format");
        return new Dictionary<string, Color>(dictionary.Select(x => 
            new KeyValuePair<string, Color>(x.Key, Color.FromArgb(x.Value.R, x.Value.G, x.Value.B))));
    }

    private string GenPath(Guid worldId)
    {
        return _rootFolder.GetPath("worlds" + Path.DirectorySeparatorChar + worldId + ".bmp");
    }


    private record Rgb(byte R, byte G, byte B);
}