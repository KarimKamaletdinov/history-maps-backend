namespace HistoryMaps;

public class World
{
    public Guid Id { get; set; }

    public MapArea Water { get; }

    public List<Country> Countries;

    public World(Guid id, MapArea water, IEnumerable<Country> countries)
    {
        Id = id;
        Water = water;
        Countries = countries.ToList();
    }

    public World(WorldDto dto)
    {
        Id = dto.Id;
        Water = new MapArea(dto.Water);
        Countries = dto.Countries.Select(x => new Country(x)).ToList();
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

    public WorldDto ToDto()
    {
        return new(Id, Water.ToDto(), Countries.Select(x => x.ToDto()).ToArray());
    }

    public World Copy(Guid newId)
    {
        return new World(newId, Water.Copy(), Countries.Select(x => x.Copy()));
    }
}