namespace HistoryMaps;

public class GitPushCommandHandler : ICommandHandler<GitPush>
{
    private readonly ExecuteGitCommandService _execute;

    public GitPushCommandHandler(ExecuteGitCommandService execute)
    {
        _execute = execute;
    }

    public void Execute(GitPush command)
    {
        _execute.ExecuteGitCommand("push -q", command.Directory);
    }
}