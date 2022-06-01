using System.Resources;
using System.Runtime.InteropServices.ComTypes;

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
                createChanges.Add(new (c));
            }
        }
        foreach (var country in baseWorld.Countries)
        {
            if (changedWorld.Countries.All(x => x != country))
            {
                dropChanges.Add(new (country.Name));
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
                        conquestChanges.Add(new (
                            newCountry?.Name,
                            oldCountry == null ? null : oldCountry.Name,
                            new (points)));
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
        return new (Year, Name, WorldId);
    }
}