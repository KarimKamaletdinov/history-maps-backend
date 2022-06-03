using System.Diagnostics;

namespace HistoryMaps;

public class GitCloneCommandHandler : ICommandHandler<GitClone>
{
    private readonly IRootFolderProvider _rootFolder;

    public GitCloneCommandHandler(IRootFolderProvider rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public void Execute(GitClone command)
    {
        using var process = new Process();
        process.StartInfo.FileName = "git";
        process.StartInfo.Arguments = $"clone {command.Repository} {command.Directory}";
        process.StartInfo.WorkingDirectory = _rootFolder.GetPath("temp", "app");
    }
}