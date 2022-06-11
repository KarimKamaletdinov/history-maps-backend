namespace HistoryMaps;

public interface IChange
{
    public void Apply(World world);
    public IEnumerable<string> GetChangedCountries(World baseWorld);
}