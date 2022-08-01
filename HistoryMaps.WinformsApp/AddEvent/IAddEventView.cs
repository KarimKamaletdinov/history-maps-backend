namespace HistoryMaps;

public interface IAddEventView
{
    public event Action<int, int?, string> Save;
    public event Action Cancel;
}