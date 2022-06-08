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

    public void Insert(WorldDto world)
    {
        var (doc, id) = To3MfConverter.Convert(world);
        Save3Mf(doc, _rootFolder.GetPath("worlds", id + ".3mf"));
    }

    public void InsertSeparately(WorldDto world)
    {       
        Directory.CreateDirectory(_rootFolder.GetPath("worlds", world.Id.ToString()));
        File.WriteAllText(_rootFolder.GetPath("worlds", world.Id.ToString(), "countries.json"),
            JsonConvert.SerializeObject(new JsonFileWorld
            {
                Countries = world.Countries.Select(x => x.Name)
            }));

        var data = To3MfConverter.ConvertSeparately(world);
        Save3Mf(data.Base, _rootFolder.GetPath("worlds", world.Id.ToString(), "base.3mf"));

        foreach (var country in data.Countries)
        {
            Save3Mf(country, _rootFolder.GetPath("worlds", world.Id.ToString(), country.Metadata + ".3mf"));
        }
    }

    public void ClearAll()
    {
        foreach (var file in Directory.GetFiles(_rootFolder.GetPath("worlds")))
            if (file.EndsWith(".3mf"))
                File.Delete(file);
        foreach (var directory in Directory.GetDirectories(_rootFolder.GetPath("worlds")))
            Directory.Delete(directory, true);
    }

    private void Save3Mf(Document document, string resultPath)
    {
        var tempId = Guid.NewGuid().ToString();
        var tempFolderPath = _rootFolder.GetPath("temp", tempId);
        if(Directory.Exists(tempFolderPath))
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
        if(File.Exists(resultPath))
            File.Delete(resultPath);
        ZipFile.CreateFromDirectory(tempFolderPath, resultPath);
        Directory.Delete(tempFolderPath, true);
    }

    private class JsonFileWorld
    {
        public IEnumerable<string> Countries { get; set; } = Array.Empty<string>();
    }
}