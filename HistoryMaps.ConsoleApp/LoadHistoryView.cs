namespace HistoryMaps;

public class LoadHistoryView
{
    private readonly ICommandHandler<LoadHistory> _loadHistory;

    public LoadHistoryView(ICommandHandler<LoadHistory> loadHistory)
    {
        _loadHistory = loadHistory;
    }

    public void Run()
    {
        _loadHistory.Execute(new ());
    }
}
