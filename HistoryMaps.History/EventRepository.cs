using System.Diagnostics.CodeAnalysis;
using Dapper;
using Dapper.Contrib.Extensions;

namespace HistoryMaps;

public class EventRepository : IEventRepository
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly IWorldBmpRepository _worldBmpRepository;
    private const string EventsTableName = "events";
    private const string ChangesTableName = "changes";
    private const string PointsTableName = "points";

    public EventRepository(IConnectionFactory connectionFactory,
        IWorldBmpRepository worldBmpRepository)
    {
        _connectionFactory = connectionFactory;
        _worldBmpRepository = worldBmpRepository;
    }

    public IReadOnlyCollection<Event> GetAllEvents()
    {
        using var connection = _connectionFactory.CreateConnection();
        var events = connection.GetAll<DbEvent>();
        var result = new List<Event>();
        for (var i = 0; i < events.Count(); i++)
        {
            var e = events.ElementAt(i);
            var changes = connection.Query<DbChange>(
                $"SELECT * FROM {ChangesTableName} WHERE event_id='{e.id}'" +
                $"ORDER BY id");
            if (i == 0)
            {
                var baseWorld = _worldBmpRepository.GetBaseWorld();
                result.Add(new Event(e.id, e.name, e.year,
                    changes.Select(x => ParseChange(baseWorld, x)).ToList(), baseWorld));
            }
            else
            {
                result.Add(new Event(e.id, e.name, e.year,
                    changes.Select(x => ParseChange(result[i - 1].World, x)).ToList(), result[i - 1]));
            }
        }
        return result;
    }

    private bool[,] GetPoints(Guid areaId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var result = new bool[Map.Width, Map.Height];
        foreach (var point in connection.Query<DbPoint>(
                     $"SELECT * FROM {PointsTableName} WHERE area_id = '{areaId}'"))
        {
            result[point.x, point.y] = true;
        }
        return result;
    }

    private IChange ParseChange(World world, DbChange change)
    {
        switch (change.type)
        {
            case "conquest":
                return new ConquestChange(
                    world.Countries.First(x => x.Name == change.country_name),
                    new Area(GetPoints(change.area_id)));
            default:
                throw new DomainException($"Invalid change type: {change.type}");
        }
    }

    [Table(EventsTableName)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private class DbEvent
    {
        [ExplicitKey]
        public Guid id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public Guid world_id { get; set; }
    }

    [Table(ChangesTableName)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private class DbChange
    {
        [ExplicitKey]
        public Guid event_id { get; set; }
        [ExplicitKey]
        public int id { get; set; }
        public string type { get; set; }
        public string country_name { get; set; }
        public Guid area_id { get; set; }
    }
    
    [Table(PointsTableName)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private class DbPoint
    {
        [ExplicitKey]
        public Guid area_id { get; set; }
        [ExplicitKey]
        public short x { get; set; }
        [ExplicitKey]
        public short y { get; set; }
    }
}