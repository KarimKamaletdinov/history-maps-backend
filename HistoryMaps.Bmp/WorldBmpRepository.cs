﻿using System.Drawing;
using System.Text;
using System.Text.Json;

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
        WriteColors(world.Id, CreateColors(world));
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
        WriteColors(world.Id, CreateColors(world));
    }

    public void Delete(Guid worldId)
    {
        var path = GenPath(worldId);
        if (!File.Exists(path))
            throw new DoesNotExistException($"File \"{path}\" doesn't exist!");
        File.Delete(path);
    }

    public World GetBaseWorld()
    {
        var colorDictionary = GetColors(
            _rootFolder.GetPath("constants", "base_world", ".mtd"));
        var path = _rootFolder.GetPath("constants", "base_world", ".bmp");
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
    }

    public World Get(Guid worldId)
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
    }

    private Dictionary<string, Color> GetColors(Guid worldId)
        => GetColors(_rootFolder.GetPath("worlds", worldId + ".mtd"));

    private Dictionary<string, Color> GetColors(string path)
    {
        var json = File.ReadAllText(path);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, Rgb>>(json) ?? 
                         throw new DomainException("Invalid colors format");
        return new Dictionary<string, Color>(dictionary.Select(x => 
            new KeyValuePair<string, Color>(x.Key, Color.FromArgb(x.Value.R, x.Value.G, x.Value.B))));
    }

    private Dictionary<string, Color> CreateColors(World world)
    {
        var result = new Dictionary<string, Color>();
        result.Add("water", world.Water.Color);
        foreach (var country in world.Countries)
        {
            result.Add(country.Name, country.Color);
        }
        return result;
    }

    private void WriteColors(Guid worldId, Dictionary<string, Color> colors)
    {
        File.WriteAllText(_rootFolder.GetPath("worlds", worldId + ".mtd"),
            SerializeDict(colors), Encoding.UTF8);
    }

    private string GenPath(Guid worldId)
    {
        return _rootFolder.GetPath("worlds" + Path.DirectorySeparatorChar + worldId + ".bmp");
    }

    private static string SerializeDict(Dictionary<string, Color> dict)
    {
        var result = "{\n";
        foreach (var (name, color) in dict)
        {
            result += $"    \"{name}\": {{\"R\": {color.R}, \"G\": {color.G}," +
                      $"\"B\": {color.B}}},\n";
        }

        result = result.Substring(0, result.Length - 2) + "\n}";
        return result;
    }

    private record Rgb(byte R, byte G, byte B);
}