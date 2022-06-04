namespace HistoryMaps;

public class GitCommitCommandHandler : ICommandHandler<GitCommit>
{
    private readonly ExecuteGitCommandService _execute;

    public GitCommitCommandHandler(ExecuteGitCommandService execute)
    {
        _execute = execute;
    }

    public void Execute(GitCommit command)
    {
        _execute.ExecuteGitCommand("add *", command.Directory);
        _execute.ExecuteGitCommand($"commit -m \"{command.Name}\"", command.Directory);
    }
}