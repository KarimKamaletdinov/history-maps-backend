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
        //var data = Mapper.Convert(world);
        var (document, _) = To3MfConverter.Convert(world);

        var points = new List<float>();
        var colors = new List<float>();
        var countries =
            new List<JsonFileCountry>(world.Countries.Select(x =>
                new JsonFileCountry(x.Name, new(x.Color.R, x.Color.G, x.Color.B))))
            {
                new JsonFileCountry("water", new(world.Water.Color.R, world.Water.Color.G, world.Water.Color.B))
            };

        foreach (var triangle in document.Triangles)
        {
            points.Add(document.Vertices[triangle.V1].X);
            points.Add(document.Vertices[triangle.V1].Y);
            points.Add(document.Vertices[triangle.V1].Z);
            colors.Add(document.Colors[triangle.Color].R / 255f);
            colors.Add(document.Colors[triangle.Color].G / 255f);
            colors.Add(document.Colors[triangle.Color].B / 255f);

            points.Add(document.Vertices[triangle.V2].X);
            points.Add(document.Vertices[triangle.V2].Y);
            points.Add(document.Vertices[triangle.V2].Z);
            colors.Add(document.Colors[triangle.Color].R / 255f);
            colors.Add(document.Colors[triangle.Color].G / 255f);
            colors.Add(document.Colors[triangle.Color].B / 255f);

            points.Add(document.Vertices[triangle.V3].X);
            points.Add(document.Vertices[triangle.V3].Y);
            points.Add(document.Vertices[triangle.V3].Z);
            colors.Add(document.Colors[triangle.Color].R / 255f);
            colors.Add(document.Colors[triangle.Color].G / 255f);
            colors.Add(document.Colors[triangle.Color].B / 255f);
        }

        File.WriteAllBytes(_rootFolder.GetPath("worlds", world.Id.ToString(), "points.bin"), ListToBytes(points).ToArray());
        File.WriteAllBytes(_rootFolder.GetPath("worlds", world.Id.ToString(), "colors.bin"), ListToBytes(colors).ToArray());

        File.WriteAllText(_rootFolder.GetPath("worlds", world.Id.ToString(), "countries.json"), 
            JsonConvert.SerializeObject(countries));
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
            var countries = new List<JsonFileCountry>(world.Countries.Select(x => new JsonFileCountry(x.Name, new(x.Color.R, x.Color.G, x.Color.B))))
            {
                new("water", new(world.Water.Color.R, world.Water.Color.G, world.Water.Color.B))
            };

            return new(countries, CreatePoints(), CreateColors(world));
        }

        private static IEnumerable<float> CreatePoints()
        {
            for (var x = 0; x < Map.Width; x++)
            {
                for (var y = 0; y < Map.Height; y++)
                {
                    var triangle = CreateTriangle(x, y);
                    yield return triangle.Item1.X;
                    yield return triangle.Item1.Y;
                    yield return triangle.Item1.Z;

                    yield return triangle.Item2.X;
                    yield return triangle.Item2.Y;
                    yield return triangle.Item2.Z;

                    yield return triangle.Item3.X;
                    yield return triangle.Item3.Y;
                    yield return triangle.Item3.Z;
                }
            }
        }

        private static IEnumerable<float> CreateColors(WorldDto world)
        {
            for (var x = 0; x < Map.Width; x++)
            {
                for (var y = 0; y < Map.Height; y++)
                {
                    var country = world.Countries.FirstOrDefault(c => c.Points[x, y]);
                    if (country != null)
                    {
                        yield return (float)country.Color.R / 255;
                        yield return (float)country.Color.G / 255;
                        yield return (float)country.Color.B / 255;
                        
                        yield return (float)country.Color.R / 255;
                        yield return (float)country.Color.G / 255;
                        yield return (float)country.Color.B / 255;
                        
                        yield return (float)country.Color.R / 255;
                        yield return (float)country.Color.G / 255;
                        yield return (float)country.Color.B / 255;
                    }
                    else if (world.Water.Points[x, y])
                    {
                        yield return (float)world.Water.Color.R / 255;
                        yield return (float)world.Water.Color.G / 255;
                        yield return (float)world.Water.Color.B / 255;
                        
                        yield return (float)world.Water.Color.R / 255;
                        yield return (float)world.Water.Color.G / 255;
                        yield return (float)world.Water.Color.B / 255;
                        
                        yield return (float)world.Water.Color.R / 255;
                        yield return (float)world.Water.Color.G / 255;
                        yield return (float)world.Water.Color.B / 255;
                    }
                    else
                    {
                        yield return 1;
                        yield return 1;
                        yield return 1;
                        
                        yield return 1;
                        yield return 1;
                        yield return 1;
                        
                        yield return 1;
                        yield return 1;
                        yield return 1;
                    }
                }
            }
        }

        private static (Vertex, Vertex, Vertex) CreateTriangle(int x, int y)
        {
            if (y % 2 == 0)
            {
                if (x % 2 == 0)
                {
                    return (
                        CreateVertex(x - 1, y),
                        CreateVertex(x, y + 1),
                        CreateVertex(x + 1, y));
                }
                return (
                    CreateVertex(x - 1, y + 1),
                    CreateVertex(x + 1, y + 1),
                    CreateVertex(x, y));
            }
            if (x % 2 == 0)
            {
                return (
                    CreateVertex(x - 1, y + 1),
                    CreateVertex(x + 1, y + 1),
                    CreateVertex(x, y));
            }

            return (
                CreateVertex(x - 1, y),
                CreateVertex(x, y + 1),
                CreateVertex(x + 1, y));
        }

        private static Vertex CreateVertex(float y, float x)
        {
            var vertex = Matrix.RotateZ((x - 180) / 180 * MathF.PI)
                .Multiply(Matrix.RotateY((y - 90) / 180 * MathF.PI)
                    .Multiply(new(500, 0, 0)));
            return vertex;
        }
    }
}