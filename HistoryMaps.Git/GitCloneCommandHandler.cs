namespace HistoryMaps;

public class GitCloneCommandHandler : ICommandHandler<GitClone>
{
    private readonly ExecuteGitCommandService _execute;

    public GitCloneCommandHandler(ExecuteGitCommandService execute)
    {
        _execute = execute;
    }

    public Task Execute(GitClone command)
    {
        return _execute.ExecuteGitCommand($"clone {command.Repository} {command.Directory} -q");
    }
}