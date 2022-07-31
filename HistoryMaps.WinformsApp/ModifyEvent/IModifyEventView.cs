namespace HistoryMaps;

public interface IModifyEventView
{
    public WorldDto World { set; }
    public event Action<Guid> Save;
    public event Action Back;
}