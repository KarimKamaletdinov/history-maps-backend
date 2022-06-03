namespace HistoryMaps;

public interface IRootFolderProvider
{
    public string GetRootFolder();
}

public interface IGitRemoteUrlProvider
{
    public string GetGitRemoteUrl();
}


public static class RootFolderProviderExtensions
{
    public static string GetPath(this IRootFolderProvider rootFolder, string path)
    {
        return rootFolder.GetRootFolder() + path;
    }
    public static string GetPath(this IRootFolderProvider rootFolder, params string[] path)
    {
        return rootFolder.GetPath(string.Join(Path.DirectorySeparatorChar, path));
    }
}