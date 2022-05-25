namespace HistoryMaps;

public class World
{
    private readonly List<Country> _countries;

    public Guid Id { get; set; }

    public Area Water { get; }

    public IReadOnlyCollection<Country> Countries => _countries;

    public World(Guid id, Area water, IEnumerable<Country> countries)
    {
        Id = id;
        Water = water;
        _countries = countries.ToList();
    }

    public WorldDto ToDto()
    {
        return new(Id, Water.ToDto(), Countries.Select(x => x.ToDto()).ToArray());
    }
}