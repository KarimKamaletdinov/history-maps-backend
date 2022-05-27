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
}