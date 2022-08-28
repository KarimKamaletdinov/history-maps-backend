using System.Drawing;

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

    private record WorldData(IEnumerable<float> Points, IEnumerable<float> Colors);

    private static class Mapper
    {
        public static WorldData Convert(WorldDto world)
        {
            var points = new List<float>();
            var colors = new List<float>();

            var worldColors = new List<Color>();
            worldColors.AddRange(world.Countries.Select(x => x.Color));
            worldColors.Add(world.Water.Color);
            worldColors.Add(Color.White);
            
            for (var x = 0; x < Map.Width; x++)
            {
                for (var y = 0; y < Map.Height; y++)
                {
                    var country = world.Countries.FirstOrDefault(c => c.Points[x, y]);
                    if (country != null)
                        AddPoint(x, y, country.Color);
                    else if (world.Water.Points[x, y])
                        AddPoint(x, y, world.Water.Color);
                    else
                        AddPoint(x, y, Color.White);
                }
            }

            void AddPoint(int x, int y, Color color)
            {
                if (y % 2 == 0 ^ x % 2 == 0)
                {
                    AddVertex(x - 1, y + 1);
                    AddVertex(x + 1, y + 1);
                    AddVertex(x, y);
                }
                else
                {
                    AddVertex(x - 1, y);
                    AddVertex(x, y + 1);
                    AddVertex(x + 1, y);
                }

                void AddVertex(int vx, int vy)
                {
                    var colorId = worldColors.IndexOf(color);
                    var vertex = Matrix.RotateZ(((float)vx - 180) / 180 * MathF.PI)
                        .Multiply(Matrix.RotateY(((float)vy - 90) / 180 * MathF.PI)
                            .Multiply(new(500 + worldColors.Count - colorId, 0, 0, color)));
                    points.Add(vertex.X);
                    points.Add(vertex.Y);
                    points.Add(vertex.Z);
                    colors.Add(vertex.Color.R);
                    colors.Add(vertex.Color.G);
                    colors.Add(vertex.Color.B);
                }
            }
            return new(points, colors);
        }
    }
}