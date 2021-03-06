using System.IO.Compression;
using Newtonsoft.Json;

namespace HistoryMaps;

public class World3MfRepository : IWorld3MfRepository
{
    private readonly IRootFolderProvider _rootFolder;

    public World3MfRepository(IRootFolderProvider rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public Task InsertSeparately(WorldDto world) => Task.Run(() =>
    {
        Directory.CreateDirectory(_rootFolder.GetPath("worlds", world.Id.ToString()));
        File.WriteAllText(_rootFolder.GetPath("worlds", world.Id.ToString(), "countries.json"),
            JsonConvert.SerializeObject(new JsonFileWorld(
                world.Countries.Select(x => new JsonFileCountry(x.Name,
                    new(x.Color.R, x.Color.G, x.Color.B)))
            ), Formatting.Indented));

        var data = To3MfConverter.ConvertSeparately(world);
        Save3Mf(data.Base, _rootFolder.GetPath("worlds", world.Id.ToString(), "base.3mf"));

        foreach (var country in data.Countries)
        {
            Save3Mf(country, _rootFolder.GetPath("worlds", world.Id.ToString(), country.Metadata + ".3mf"));
        }
    });

    public Task ClearAll() => Task.Run(() =>
    {
        foreach (var file in Directory.GetFiles(_rootFolder.GetPath("worlds")))
            if (file.EndsWith(".3mf"))
                File.Delete(file);
        foreach (var directory in Directory.GetDirectories(_rootFolder.GetPath("worlds")))
            Directory.Delete(directory, true);
    });

    public Task<IEnumerable<Guid>> GetAllIds() => Task.Run(() =>
    {
        var result = new List<Guid>();
        foreach (var directory in Directory.GetDirectories(_rootFolder.GetPath("worlds")))
            if (Guid.TryParse(directory.Split(Path.DirectorySeparatorChar).Last(), out var guid))
                result.Add(guid);
        return (IEnumerable<Guid>)result;
    });

    private void Save3Mf(Document document, string resultPath)
    {
        var tempId = Guid.NewGuid().ToString();
        var tempFolderPath = _rootFolder.GetPath("temp", tempId);
        if (Directory.Exists(tempFolderPath))
            Directory.Delete(tempFolderPath, true);
        ZipFile.ExtractToDirectory(_rootFolder.GetPath("constants", "template.3mf"),
            tempFolderPath);
        var tPath = _rootFolder.GetPath("temp", tempId, "3D", "3dmodel.model");
        var template = File.ReadAllText(tPath);
        var data = template
            .Replace("<!--metadata-->", document.Metadata)
            .Replace("<!--colors-->", Xml.Convert(document.Colors))
            .Replace("<!--vertices-->", Xml.Convert(document.Vertices))
            .Replace("<!--triangles-->", Xml.Convert(document.Triangles));
        File.WriteAllText(tPath, data);
        if (File.Exists(resultPath))
            File.Delete(resultPath);
        ZipFile.CreateFromDirectory(tempFolderPath, resultPath);
        Directory.Delete(tempFolderPath, true);
    }

    private record JsonFileWorld(IEnumerable<JsonFileCountry> Countries);

    private record JsonFileCountry(string Name, JsonFileColor Color);

    private record JsonFileColor(int R, int G, int B);
}