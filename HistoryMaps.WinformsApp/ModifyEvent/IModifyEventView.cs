namespace HistoryMaps;

public interface IModifyEventView
{
    public WorldDto World { set; }
    public event Action<WorldDto> Save;
}