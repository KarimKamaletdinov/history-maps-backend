namespace HistoryMaps;

public class LoadAddedHistoryView
{
    private readonly ICommandHandler<LoadAddedHistory> _loadHistory;

    public LoadAddedHistoryView(ICommandHandler<LoadAddedHistory> loadHistory)
    {
        _loadHistory = loadHistory;
    }

    public void Run()
    {
        _loadHistory.Execute(new ());
    }
}