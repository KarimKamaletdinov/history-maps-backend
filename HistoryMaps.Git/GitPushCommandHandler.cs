namespace HistoryMaps;

public class GitPushCommandHandler : ICommandHandler<GitPush>
{
    private readonly ExecuteGitCommandService _execute;

    public GitPushCommandHandler(ExecuteGitCommandService execute)
    {
        _execute = execute;
    }

    public Task Execute(GitPush command)
    {
        return _execute.ExecuteGitCommand("push -q", command.Directory);
    }
}