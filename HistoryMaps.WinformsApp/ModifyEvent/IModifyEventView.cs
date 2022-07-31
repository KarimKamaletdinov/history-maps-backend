namespace HistoryMaps;

public interface IModifyEventView
{
    public WorldBitmapDto World { set; }
    public event Action<WorldBitmapDto> Save;
    public event Action Back;
}