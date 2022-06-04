using System.Diagnostics;

namespace HistoryMaps;

public class GitCloneCommandHandler : ICommandHandler<GitClone>
{
    private readonly ExecuteGitCommandService _execute;

    public GitCloneCommandHandler(ExecuteGitCommandService execute)
    {
        _execute = execute;
    }

    public void Execute(GitClone command)
    {
        _execute.ExecuteGitCommand($"clone {command.Repository} {command.Directory}");
    }
}