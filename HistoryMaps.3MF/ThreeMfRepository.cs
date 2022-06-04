using System.IO.Compression;

namespace HistoryMaps;

public class ThreeMfRepository : IThreeMfRepository
{
    private readonly IRootFolderProvider _rootFolder;

    public ThreeMfRepository(IRootFolderProvider rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public void Insert(Document document, Guid id)
    {
        var tempFolderPath = _rootFolder.GetPath("temp", id.ToString());
        if(Directory.Exists(tempFolderPath))
            Directory.Delete(tempFolderPath, true);
        ZipFile.ExtractToDirectory(_rootFolder.GetPath("constants", "template.3mf"), 
            tempFolderPath);
        var tPath = _rootFolder.GetPath("temp", id.ToString(), "3D", "3dmodel.model");
        var template = File.ReadAllText(tPath);
        var data = template
            .Replace("<!--colors-->", Xml.Convert(document.Colors))
            .Replace("<!--vertices-->", Xml.Convert(document.Vertices))
            .Replace("<!--triangles-->", Xml.Convert(document.Triangles));
        File.WriteAllText(tPath, data);
        ZipFile.CreateFromDirectory(_rootFolder.GetPath("temp", id.ToString()), _rootFolder.GetPath("worlds",
            id + ".3mf"));
    }

    private static void CopyFiles(string sourcePath, string targetPath)
    {
        //Now Create all of the directories
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }

        //Copy all the files & Replaces any files with the same name
        foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }
}