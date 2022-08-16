namespace HistoryMaps;

public class SaveChangesToGitRepoCommandHandler : ICommandHandler<SaveChangesToGitRepo>
{
    private readonly IGitRemoteUrlProvider _remoteUrlProvider;
    private readonly IRootFolderProvider _rootFolderProvider;
    private readonly ICommandHandler<GitCommit> _gitCommit;
    private readonly ICommandHandler<GitPush> _gitPush;

    public SaveChangesToGitRepoCommandHandler(IGitRemoteUrlProvider remoteUrlProvider, IRootFolderProvider rootFolderProvider,
        ICommandHandler<GitCommit> gitCommit,  ICommandHandler<GitPush> gitPush)
    {
        _remoteUrlProvider = remoteUrlProvider;
        _rootFolderProvider = rootFolderProvider;
        _gitCommit = gitCommit;
        _gitPush = gitPush;
    }

    public void Execute(SaveChangesToGitRepo command)
    {
        _gitCommit.Execute(new (_rootFolderProvider.GetPath("worlds"), $"Commit by HistoryMaps at {DateTime.Now:G}"));
        _gitPush.Execute(new (_rootFolderProvider.GetPath("worlds")));
    }
}