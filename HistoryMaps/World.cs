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

    public World(WorldDto dto)
    {
        Id = dto.Id;
        Water = new Area(dto.Water);
        _countries = dto.Countries.Select(x => new Country(x)).ToList();
    }

    public WorldDto ToDto()
    {
        return new(Id, Water.ToDto(), Countries.Select(x => x.ToDto()).ToArray());
    }

    public void SetPoint(int x, int y, Country? country)
    {
        if (Water.Points[x, y])
            throw new DomainException("Can't set a point filled by water");
        foreach (var c in Countries)
            if (c.Points[x, y])
                c.Points[x, y] = false;
        if (country != null)
        {
            if (country.Points[x, y])
                throw new DomainException("The country you are setting to " +
                                          "already owns this point");
            country.Points[x, y] = true;
        }
    }
}