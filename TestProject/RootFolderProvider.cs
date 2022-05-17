namespace HistoryMaps;

public class RootFolderProvider : IRootFolderProvider
{
    public string GetRootFolder()
    {
        return "data\\";
    }
}