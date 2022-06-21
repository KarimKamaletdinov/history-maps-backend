namespace HistoryMaps;

public class GitPullCommandHandler : ICommandHandler<GitPull>
{
    private readonly ExecuteGitCommandService _execute;

    public GitPullCommandHandler(ExecuteGitCommandService execute)
    {
        _execute = execute;
    }

    public void Execute(GitPull command)
    {
        _execute.ExecuteGitCommand("pull -q", command.Directory);
    }
}