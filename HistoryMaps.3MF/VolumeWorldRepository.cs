using System.Drawing;
using System.Globalization;
using System.IO.Compression;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace HistoryMaps;

public class VolumeWorldRepository : IVolumeWorldRepository
{
    private readonly IRootFolderProvider _rootFolder;

    public VolumeWorldRepository(IRootFolderProvider rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public void Insert(WorldDto world)
    {
        Directory.CreateDirectory(_rootFolder.GetPath("worlds", world.Id.ToString()));

        var data = Mapper.Convert(world);

        File.WriteAllBytes(_rootFolder.GetPath("worlds", world.Id.ToString(), "points.bin"), ListToBytes(data.Points).ToArray());
        File.WriteAllBytes(_rootFolder.GetPath("worlds", world.Id.ToString(), "colors.bin"), ListToBytes(data.Colors).ToArray());
        
        File.WriteAllText(_rootFolder.GetPath("worlds", world.Id.ToString(), "countries.json"),
            JsonConvert.SerializeObject(data.Countries));
    }

    public void ClearAll()
    {
        foreach (var file in Directory.GetFiles(_rootFolder.GetPath("worlds")))
            if (file.EndsWith(".3mf"))
                File.Delete(file);
        foreach (var directory in Directory.GetDirectories(_rootFolder.GetPath("worlds")))
            Directory.Delete(directory, true);
    }

    public IEnumerable<Guid> GetAllIds()
    {
        foreach (var directory in Directory.GetDirectories(_rootFolder.GetPath("worlds")))
            if (Guid.TryParse(directory.Split(Path.DirectorySeparatorChar).Last(), out var guid))
                yield return guid;
    }

    private static IEnumerable<byte> ListToBytes(IEnumerable<float> list)
    {
        return list.SelectMany(BitConverter.GetBytes);
    }

    private record WorldData(IEnumerable<JsonFileCountry> Countries, IEnumerable<float> Points, IEnumerable<float> Colors);

    private record JsonFileCountry(string Name, JsonFileColor Color);

    private record JsonFileColor(int R, int G, int B);

    private static class Mapper
    {
        public static WorldData Convert(WorldDto world)
        {
            var data = ToVolumeConverter.Convert(world);

            var points = new List<float>();
            var colors = new List<float>();
            var countries =
                new List<JsonFileCountry>(world.Countries.Select(x =>
                    new JsonFileCountry(x.Name, new(x.Color.R, x.Color.G, x.Color.B))))
                {
                    new JsonFileCountry("water", new(world.Water.Color.R, world.Water.Color.G, world.Water.Color.B))
                };

            foreach (var triangle in data.Triangles)
            {
                points.Add(triangle.V1.X);
                points.Add(triangle.V1.Y);
                points.Add(triangle.V1.Z);
                colors.Add(triangle.Color.R / 255f);
                colors.Add(triangle.Color.G / 255f);
                colors.Add(triangle.Color.B / 255f);
            
                points.Add(triangle.V2.X);
                points.Add(triangle.V2.Y);
                points.Add(triangle.V2.Z);
                colors.Add(triangle.Color.R / 255f);
                colors.Add(triangle.Color.G / 255f);
                colors.Add(triangle.Color.B / 255f);
            
                points.Add(triangle.V3.X);
                points.Add(triangle.V3.Y);
                points.Add(triangle.V3.Z);
                colors.Add(triangle.Color.R / 255f);
                colors.Add(triangle.Color.G / 255f);
                colors.Add(triangle.Color.B / 255f);
            }
            return new(countries, points, colors);
        }
    }
}