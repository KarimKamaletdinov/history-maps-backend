namespace HistoryMaps;

public class CopyDataToWebAppCommandHandler : ICommandHandler<CopyDataToWebApp>
{
    private readonly IRootFolderProvider _rootFolderProvider;

    public CopyDataToWebAppCommandHandler(IRootFolderProvider rootFolderProvider)
    {
        _rootFolderProvider = rootFolderProvider;
    }

    public void Execute(CopyDataToWebApp command)
    {
        if(Directory.Exists(_rootFolderProvider.GetPath("app", "data")))
            Directory.Delete(_rootFolderProvider.GetPath("app", "data"), true);
        Directory.CreateDirectory(_rootFolderProvider.GetPath("app", "data"));
        File.Copy(_rootFolderProvider.GetPath("events.json"), 
            _rootFolderProvider.GetPath("app", "data", "events.json"));
        CopyDirectory(_rootFolderProvider.GetPath("worlds"),
            _rootFolderProvider.GetPath("app", "data", "worlds"));
    }

    private static void CopyDirectory(string from, string to)
    {
        Directory.CreateDirectory(to);
        foreach (var file in Directory.GetFiles(from)) 
            File.Copy(file, to + file.Replace(from, ""), true);

        foreach (var directory in Directory.GetDirectories(from))
            CopyDirectory(directory, to + directory.Replace(from, ""));
    }
}