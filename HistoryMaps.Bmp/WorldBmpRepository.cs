using System.Drawing;
using System.Text;
using Newtonsoft.Json;

namespace HistoryMaps;

public class WorldBmpRepository : IWorldBmpRepository
{
    private readonly IRootFolderProvider _rootFolder;

    public WorldBmpRepository(IRootFolderProvider rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public Task Insert(World world) => Task.Run(() =>
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
                    foreach (var country in world.Countries.Where(country => country.Points[x, y]))
                        image.SetPixel(x, y, country.Color);
            }
        }

        image.Save(path);
        WriteColors(world.Id, CreateColors(world));
    });

    public Task InsertBitmap(Guid id, WorldBitmapDto world) => Task.Run(() =>
    {
        var path = GenPath(id);

        if (File.Exists(path))
            throw new AlreadyExistsException($"File \"{path}\" already exists!");
        world.Bitmap.Save(path);
        var colors = new Dictionary<string, Color>(world.Countries.Select(x =>
            new KeyValuePair<string, Color>(x.Name, x.Color)))
        {
            { "water", Map.WaterColor }
        };
        WriteColors(id, colors);
    });

    public Task ClearAll() => Task.Run(() =>
    {
        foreach (var file in Directory.GetFiles(_rootFolder.GetPath("worlds")))
            File.Delete(file);
    });

    public Task<IEnumerable<Guid>> GetAllIds() => Task.Run(() =>
    {
        var result = new List<Guid>();
        foreach (var file in Directory.GetFiles(_rootFolder.GetPath("worlds")))
            if (file.EndsWith(".bmp"))
                if (Guid.TryParse(file.Split(Path.DirectorySeparatorChar).Last().Replace(".bmp", ""), out var guid))
                    result.Add(guid);
        return (IEnumerable<Guid>)result;
    });

    public Task<World> GetBaseWorld() => Task.Run(() =>
    {
        var colorDictionary = GetColors(
            _rootFolder.GetPath("constants", "base_world.json"));
        var path = _rootFolder.GetPath("constants", "base_world.bmp");
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");

        using var image = (Bitmap)Image.FromFile(path);

        var water = new MapArea(new bool[Map.Width, Map.Height], colorDictionary["water"]);
        var countries = new List<Country>();
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

        return new World(Guid.Empty, water, countries);
    });

    public Task<World> Get(Guid worldId) => Task.Run(() =>
    {
        var colorDictionary = GetColors(worldId);
        var path = GenPath(worldId);
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");

        using var image = (Bitmap)Image.FromFile(path);

        var water = new MapArea(new bool[Map.Width, Map.Height], colorDictionary["water"]);
        var countries = new List<Country>();
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
    });

    public Task<WorldBitmapDto> GetBitmap(Guid id) => Task.Run(() =>
    {
        var colorDictionary = GetColors(id);
        var path = GenPath(id);
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");
        return new WorldBitmapDto((Bitmap)Image.FromFile(path),
            colorDictionary.Where(x => x.Key != "water").Select(x => new CountryColorDto(x.Key, x.Value)));
    });

    private Dictionary<string, Color> GetColors(Guid worldId)
        => GetColors(_rootFolder.GetPath("worlds", worldId + ".json"));

    private static Dictionary<string, Color> GetColors(string path)
    {
        var json = File.ReadAllText(path);
        var countryColors = JsonConvert.DeserializeObject<List<CountryColor>>(json) ?? 
                         throw new DomainException("Invalid colors format");
        return new(countryColors.Select(x => 
            new KeyValuePair<string, Color>(x.Name,
                Color.FromArgb(x.Color.R, x.Color.G, x.Color.B))));
    }

    private static Dictionary<string, Color> CreateColors(World world)
    {
        var result = new Dictionary<string, Color> { { "water", world.Water.Color } };
        foreach (var country in world.Countries)
        {
            result.Add(country.Name, country.Color);
        }
        return result;
    }

    private void WriteColors(Guid worldId, Dictionary<string, Color> colors)
    {
        File.WriteAllText(_rootFolder.GetPath("worlds", worldId + ".json"),
            JsonConvert.SerializeObject(colors.Select(x => 
                new CountryColor(x.Key, new (x.Value.R, x.Value.G, x.Value.B))), 
                Formatting.Indented),
            Encoding.UTF8);
    }

    private string GenPath(Guid worldId)
    {
        return _rootFolder.GetPath("worlds" + Path.DirectorySeparatorChar + worldId + ".bmp");
    }

    private record CountryColor(string Name, Rgb Color);

    private record Rgb(byte R, byte G, byte B);
}