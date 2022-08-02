namespace HistoryMaps;

public class GitCommitCommandHandler : ICommandHandler<GitCommit>
{
    private readonly ExecuteGitCommandService _execute;

    public GitCommitCommandHandler(ExecuteGitCommandService execute)
    {
        _execute = execute;
    }

    public async Task Execute(GitCommit command)
    {
        await _execute.ExecuteGitCommand("add *", command.Directory);
        await _execute.ExecuteGitCommand($"commit -m \"{command.Name}\" -q", command.Directory);
    }
}