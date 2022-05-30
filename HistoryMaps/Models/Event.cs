using System.Resources;
using System.Runtime.InteropServices.ComTypes;

namespace HistoryMaps;

public class Event
{
    public Guid Id { get; }
    public string Name { get; set; }
    public int Year { get; set; }
    public IReadOnlyCollection<IChange> Changes { get; }
    private readonly World _baseWorld;
    private readonly Guid _worldId;

    public World World
    {
        get
        {
            var world = _baseWorld.Copy(_worldId);
            foreach (var change in Changes)
            {
                change.Apply(world);
            }
            return world;
        }
    }

    public Event(Guid id, string name, int year, IReadOnlyCollection<IChange> changes,
        Event previous, Guid worldId)
    {
        Id = id;
        Name = name;
        Year = year;
        Changes = changes;
        _baseWorld = previous.World;
        _worldId = worldId;
    }

    public Event(Guid id, string name, int year, IReadOnlyCollection<IChange> changes,
        World baseWorld, Guid worldId)
    {
        Id = id;
        Name = name;
        Year = year;
        Changes = changes;
        _baseWorld = baseWorld;
        _worldId = worldId;
    }

    public static IEnumerable<IChange> ParseChanges(World baseWorld, World changedWorld)
    {
        var createChanges = new List<CreateCountryChange>();
        var conquestChanges = new List<ConquestChange>();
        var dropChanges = new List<DropCountryChange>();

        foreach (var country in changedWorld.Countries)
        {
            if (baseWorld.Countries.All(x => x != country))
            {
                var c = country.Copy();
                for (var x = 0; x < Map.Width; x++)
                    for (var y = 0; y < Map.Height; y++)
                        c.Points[x, y] = false;
                createChanges.Add(new CreateCountryChange(c));
            }
        }
        foreach (var country in baseWorld.Countries)
        {
            if (changedWorld.Countries.All(x => x != country))
            {
                dropChanges.Add(new DropCountryChange(country.Name));
            }
        }
        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
            {
                var oldCountry = baseWorld.GetCountry(x, y);
                var newCountry = changedWorld.GetCountry(x, y);

                if (oldCountry != newCountry)
                {
                    if (oldCountry == null)
                    {
                        var createChange = createChanges.FirstOrDefault(c => c.NewCountry == newCountry);
                        if (createChange != null)
                        {
                            createChange.NewCountry.Points[x, y] = true;
                            continue;
                        }
                    }
                    var conquestChange = conquestChanges.FirstOrDefault(c => c.ConqueredName == oldCountry?.Name &&
                                                                             c.ConquerorName == newCountry?.Name);
                    if (conquestChange != null)
                    {
                        conquestChange.ConqueredArea!.Points[x, y] = true;
                        continue;
                    }
                    var points = new bool[Map.Width, Map.Height];
                    points[x, y] = true;
                        conquestChanges.Add(new ConquestChange(
                            newCountry?.Name,
                            oldCountry == null ? null : oldCountry.Name,
                            new Area(points)));
                }
            }

        foreach (var change in conquestChanges)
        {
            if (change.ConqueredName != null)
            {
                var change1 = change;
                var conquered = changedWorld.Countries.First(x => x.Name == change1.ConqueredName);
                var allSame = true;
                for (var x = 0; x < Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                    if (conquered.Points[x, y] ^ change.ConqueredArea!.Points[x, y])
                        allSame = false;

                if (allSame) 
                    change.ConqueredArea = null;
            }
        }

        var changes = createChanges.Cast<IChange>().ToList();
        changes.AddRange(conquestChanges);
        changes.AddRange(dropChanges);
        return changes;
    }

    public EventDto ToDto()
    {
        return new(Id, Name, Year, _worldId);
    }
}