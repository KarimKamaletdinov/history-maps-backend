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

        var data = Mapper.ConvertCountries(world);

        foreach (var country in data.Areas)
        {
            Directory.CreateDirectory(_rootFolder.GetPath("worlds", world.Id.ToString(), country.Name));
            File.WriteAllBytes(_rootFolder.GetPath("worlds", world.Id.ToString(), country.Name, "points.bin"), ListToBytes(country.Points).ToArray());
        }
    }

    public void InsertBaseWorld(WorldDto world)
    {
        Directory.CreateDirectory(_rootFolder.GetPath("worlds", "baseworld"));

        var data = Mapper.ConvertBase(world);

        foreach (var area in data.Areas)
        {
            Directory.CreateDirectory(_rootFolder.GetPath("worlds", "baseworld", area.Name));
            File.WriteAllBytes(_rootFolder.GetPath("worlds", "baseworld", area.Name, "points.bin"), ListToBytes(area.Points).ToArray());
        }
    }

    public void ClearAll()
    {
        foreach (var file in Directory.GetFiles(_rootFolder.GetPath("worlds")))
            if (file.EndsWith(".3mf"))
                File.Delete(file);
        foreach (var directory in Directory.GetDirectories(_rootFolder.GetPath("worlds")))
            if(!directory.EndsWith(".git"))
                Directory.Delete(directory, true);
    }

    public IEnumerable<Guid> GetAllIds()
    {
        foreach (var directory in Directory.GetDirectories(_rootFolder.GetPath("worlds")))
            if (Guid.TryParse(directory.Split(Path.DirectorySeparatorChar).Last(), out var guid))
                yield return guid;
    }

    public bool BaseWorldExists()
    {
        return Directory.Exists(_rootFolder.GetPath("worlds", "baseworld"));
    }

    private static IEnumerable<byte> ListToBytes(IEnumerable<float> list)
    {
        return list.SelectMany(BitConverter.GetBytes);
    }

    private record AreaData(string Name, IEnumerable<float> Points);

    private record WorldData(IEnumerable<AreaData> Areas);

    private static class Mapper
    {
        public static WorldData ConvertBase(WorldDto world)
        {
            return new(new []{Convert("water", world.Water), Convert("blank", GetBlank(world))});
        }
        
        public static WorldData ConvertCountries(WorldDto world)
        {
            return new(world.Countries.Select(x => Convert(x.Name, x)).ToList());
        }
        
        private static AreaData Convert(string name, AreaDto area)
        {
            var data = ToVolumeConverter.Convert(area);

            var points = new List<float>();

            foreach (var triangle in data.Triangles)
            {
                points.Add(triangle.V1.X);
                points.Add(triangle.V1.Y);
                points.Add(triangle.V1.Z);
            
                points.Add(triangle.V2.X);
                points.Add(triangle.V2.Y);
                points.Add(triangle.V2.Z);
            
                points.Add(triangle.V3.X);
                points.Add(triangle.V3.Y);
                points.Add(triangle.V3.Z);
            }
                
            return new(name, points);
        }

        private static AreaDto GetBlank(WorldDto world)
        {
            var result = new bool[Map.Width, Map.Height];
            for (var x = 0; x < Map.Width; x++)
            {
                for (var y = 0; y < Map.Height; y++)
                {
                    if (!world.Water.Points[x, y] && world.Countries.All(c => !c.Points[x, y]))
                        result[x, y] = true;
                }
            }

            return new(result);
        }
    }
}