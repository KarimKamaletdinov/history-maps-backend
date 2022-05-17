namespace HistoryMaps;

public interface IRootFolderProvider
{
    /// <summary>
    /// Корневая папка для данных программы
    /// </summary>
    /// <returns>Путь к папке, заканчивающийся на разделитель (/ или \)</returns>
    public string GetRootFolder();
}

public static class RootFolderProviderExtensions
{
    public static string GetPath(this IRootFolderProvider rootFolder, string path)
    {
        return rootFolder.GetRootFolder() + path;
    }
}