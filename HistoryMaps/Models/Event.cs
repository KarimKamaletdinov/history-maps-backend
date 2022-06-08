namespace HistoryMaps;

public class Event
{
    public int Year { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<IChange> Changes { get; }
    public Guid WorldId { get; }
    private readonly World _baseWorld;

    public World World
    {
        get
        {
            var world = _baseWorld.Copy(WorldId);
            foreach (var change in Changes)
            {
                change.Apply(world);
            }
            return world;
        }
    }

    public Event(int year, string name, IReadOnlyCollection<IChange> changes,
        Event previous, Guid worldId)
    {
        Name = name;
        Year = year;
        Changes = changes;
        _baseWorld = previous.World;
        WorldId = worldId;
    }

    public Event(int year, string name, IReadOnlyCollection<IChange> changes,
        World baseWorld, Guid worldId)
    {
        Name = name;
        Year = year;
        Changes = changes;
        _baseWorld = baseWorld;
        WorldId = worldId;
    }

    public static IEnumerable<IChange> ParseChanges(World baseWorld, World changedWorld)
    {
        var conquestChanges = new List<ConquestChange>();

        var createChanges = changedWorld.Countries
            .Where(country => baseWorld.Countries.All(x => x != country))
            .Select(country => new CreateCountryChange(country.Name, country.Color)).ToList();

        var dropChanges = baseWorld.Countries
            .Where(country => changedWorld.Countries.All(x => x != country))
            .Select(country => new DropCountryChange(country.Name)).ToList();

        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
            {
                var oldCountry = baseWorld.GetCountry(x, y);
                var newCountry = changedWorld.GetCountry(x, y);

                if (oldCountry != newCountry)
                {
                    var conquestChange = conquestChanges.FirstOrDefault(c => c.ConquerorName == newCountry?.Name);
                    if (conquestChange != null)
                    {
                        conquestChange.ConqueredArea.Points[x, y] = true;
                        continue;
                    }
                    var points = new bool[Map.Width, Map.Height];
                    points[x, y] = true;
                        conquestChanges.Add(new (
                            newCountry?.Name,
                            new (points)));
                }
            }

        var changes = createChanges.Cast<IChange>().ToList();
        changes.AddRange(conquestChanges);
        changes.AddRange(dropChanges);
        return changes;
    }

    public EventDto ToDto()
    {
        return new (Year, Name, WorldId);
    }
}