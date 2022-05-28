namespace HistoryMaps;

public class RootFolderProvider : IRootFolderProvider
{
    private readonly string _rootFolder;

    public RootFolderProvider(string rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public string GetRootFolder()
    {
        return _rootFolder;
    }
}