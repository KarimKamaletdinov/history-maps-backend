namespace HistoryMaps;

public class LoadGitRepoCommandHandler : ICommandHandler<LoadGitRepo>
{
    private readonly IGitRemoteUrlProvider _remoteUrlProvider;
    private readonly IRootFolderProvider _rootFolderProvider;
    private readonly ICommandHandler<GitClone> _gitClone;
    private readonly ICommandHandler<GitPull> _gitPull;

    public LoadGitRepoCommandHandler(IGitRemoteUrlProvider remoteUrlProvider, IRootFolderProvider rootFolderProvider,
        ICommandHandler<GitClone> gitClone,  ICommandHandler<GitPull> gitPull)
    {
        _remoteUrlProvider = remoteUrlProvider;
        _rootFolderProvider = rootFolderProvider;
        _gitClone = gitClone;
        _gitPull = gitPull;
    }

    public void Execute(LoadGitRepo command)
    {
        if(Directory.Exists(_rootFolderProvider.GetPath("app")))
            _gitPull.Execute(new (_rootFolderProvider.GetPath("app")));
        else
            _gitClone.Execute(new (_remoteUrlProvider.GetGitRemoteUrl(), _rootFolderProvider.GetPath("app")));
    }
}