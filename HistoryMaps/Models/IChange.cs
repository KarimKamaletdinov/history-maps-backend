namespace HistoryMaps;

public interface IChange
{
    public abstract void Apply(World world);
    public abstract ChangeDto ToDto();
}