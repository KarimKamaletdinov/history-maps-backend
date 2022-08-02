namespace HistoryMaps;

public class GitPullCommandHandler : ICommandHandler<GitPull>
{
    private readonly ExecuteGitCommandService _execute;

    public GitPullCommandHandler(ExecuteGitCommandService execute)
    {
        _execute = execute;
    }

    public Task Execute(GitPull command)
    {
        return _execute.ExecuteGitCommand("pull -q", command.Directory);
    }
}