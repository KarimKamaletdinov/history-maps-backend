namespace HistoryMaps;

public class UpdateGitRepoCommandHandler : ICommandHandler<UpdateGitRepo>
{
    private readonly ICommandHandler<GitClone> _gitClone;

    public UpdateGitRepoCommandHandler(ICommandHandler<GitClone> gitClone)
    {
        _gitClone = gitClone;
    }

    public void Execute(UpdateGitRepo command)
    {
        //_gitClone.Execute(new GitClone())
    }
}