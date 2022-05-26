namespace HistoryMaps;

public class Event
{
    public Guid Id { get; }
    public string Name { get; set; }
    public int Year { get; set; }
    public IReadOnlyCollection<IChange> Changes { get; }
    private readonly World _baseWorld;

    public World World
    {
        get
        {
            // copy world
            var world = new World(_baseWorld.ToDto());
            foreach (var change in Changes)
            {
                change.Apply(world);
            }
            return world;
        }
    }

    public Event(Guid id, string name, int year, IReadOnlyCollection<IChange> changes,
        Event previous)
    {
        Id = id;
        Name = name;
        Year = year;
        Changes = changes;
        _baseWorld = previous.World;
    }

    public Event(Guid id, string name, int year, IReadOnlyCollection<IChange> changes,
        World baseWorld)
    {
        Id = id;
        Name = name;
        Year = year;
        Changes = changes;
        _baseWorld = baseWorld;
    }
}