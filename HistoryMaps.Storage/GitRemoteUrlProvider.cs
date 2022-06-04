namespace HistoryMaps;

public class GitRemoteUrlProvider : IGitRemoteUrlProvider
{
    private readonly string _url;

    public GitRemoteUrlProvider()
    {
        throw new ();
    }

    public GitRemoteUrlProvider(string url)
    {
        _url = url;
    }

    public string GetGitRemoteUrl()
    {
        return _url;
    }
}