using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
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

    public void Insert(Event e)
    {
        using var connection = _connectionFactory.CreateConnection();
        var eventId = GenerateId(e.Year);
        connection.Insert(new DbEvent
        {
            id = eventId,
            name = e.Name,
            world_id = e.World.Id,
            year = e.Year
        });
        var id = 1;
        foreach (var change in e.Changes)
        {
            switch (change)
            {
                case ConquestChange conquestChange:
                    var country1Name = conquestChange.ConquerorName;
                    var country2Name = conquestChange.ConqueredName;
                    Guid? areaId = conquestChange.ConqueredArea == null
                        ? null
                        : Guid.NewGuid();
                    connection.Insert(new DbChange
                    {
                        type = "conquest",
                        area_id = areaId,
                        country1_name = country1Name,
                        country2_name = country2Name,
                        event_id = eventId,
                        event_year = e.Year,
                        id = id
                    });
                    if (conquestChange.ConqueredArea != null)
                        for (var x = 0; x < Map.Width; x++)
                            for (var y = 0; y < Map.Height; y++)
                                if (conquestChange.ConqueredArea.Points[x, y])
                                    connection.Insert(new DbPoint
                                    {
                                        area_id = areaId ?? throw new NullReferenceException(),
                                        x = (short)x,
                                        y = (short)y
                                    });
                    break;
                case CreateCountryChange createCountryChange:
                    var area = Guid.NewGuid();
                    connection.Insert(new DbChange
                    {
                        area_id = area,
                        color = createCountryChange.NewCountry.Color.ToArgb()
                            .ToString("x8"),
                        country1_name = createCountryChange.NewCountry.Name,
                        event_id = eventId,
                        event_year = e.Year,
                        type = "new",
                        id = id
                    });
                    for (var x = 0; x < Map.Width; x++)
                        for (var y = 0; y < Map.Height; y++)
                            if (createCountryChange.NewCountry.Points[x, y])
                                connection.Insert(new DbPoint
                                {
                                    area_id = area,
                                    x = (short)x,
                                    y = (short)y
                                });
                    break;
                case DropCountryChange dropCountryChange:
                    connection.Insert(new DbChange
                    {
                        country1_name = dropCountryChange.DroppedCountryName,
                        type = "drop",
                        id = id,
                        event_id = eventId,
                        event_year = e.Year
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(change));
            }

            id++;
        }
    }

    public IReadOnlyCollection<Event> GetAllEvents()
    {
        using var connection = _connectionFactory.CreateConnection();
        var events = connection.Query<DbEvent>($"SELECT * FROM {EventsTableName} ORDER BY year, id");
        var result = new List<Event>();
        var i = 0;
        foreach (var e in events)
        {
            var changes = connection.Query<DbChange>(
                $"SELECT * FROM {ChangesTableName} WHERE event_year={e.year} AND event_id={e.id}" +
                "ORDER BY id");
            if (i == 0)
            {
                var baseWorld = _worldBmpRepository.GetBaseWorld();
                result.Add(new (e.year, e.name,
                    changes.Select(ParseChange).ToList(), baseWorld, e.world_id));
            }
            else
            {
                result.Add(new (e.year, e.name,
                    changes.Select(ParseChange).ToList(), result[i - 1], e.world_id));
            }
            i++;
        }
        return result;
    }

    public IReadOnlyCollection<EventDto> GetAllEventDtos()
    {
        using var connection = _connectionFactory.CreateConnection();
        var events = connection.Query<DbEvent>($"SELECT * FROM {EventsTableName} ORDER BY year, id");
        return events.Select(e => new EventDto(e.year, e.name, e.world_id)).ToList();
    }

    public EventDto? GetPrevious(int year, int? id)
    {
        id ??= GenerateId(year);
        using var connection = _connectionFactory.CreateConnection();
        if (connection.QueryFirst<DbCount>($"SELECT count(*) FROM events WHERE year = {year} AND id < {id}").count > 1)
        {
            var e = connection.QueryFirst<DbEvent>($"SELECT * FROM {EventsTableName} WHERE year = {year} AND id = {id - 1}");
            return new (e.year, e.name, e.world_id);
        }

        var prevYear = connection.QueryFirst<DbMax>($"SELECT max(year) FROM {EventsTableName} WHERE year < {year}")
            .max;
        if(prevYear == null)
            return null;
        var lastId = connection.QueryFirst<DbMax>($"SELECT max(id) FROM {EventsTableName} WHERE year = {prevYear}").max;
        if (lastId == null)
            throw new();
        var ev = connection.QueryFirst<DbEvent>($"SELECT * FROM {EventsTableName} WHERE year = {prevYear} AND id = {lastId}");
        return new (ev.year, ev.name, ev.world_id);
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

    private IChange ParseChange(DbChange change)
    {
        switch (change.type)
        {
            case "conquest":
                var area = change.area_id == null ? null
                : new Area(GetPoints((Guid)change.area_id));
                return new ConquestChange(
                    change.country1_name,
                    change.country2_name,
                    area);
            case "new":
                return new CreateCountryChange(
                    new Country(GetPoints((Guid)change.area_id!), change.country1_name!,
                        Color.FromArgb(int.Parse(change.color!, NumberStyles.HexNumber))));
            case "drop":
                return new DropCountryChange(change.country1_name!);
            default:
                throw new DomainException($"Invalid change type: {change.type}");
        }
    }

    private int GenerateId(int year)
    {
        using var connection = _connectionFactory.CreateConnection();
        return connection.QueryFirst<DbMax>($"SELECT MAX(id) FROM {EventsTableName} WHERE year = {year}").max + 1 ?? 1;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private class DbCount
    {
        public int count { get; set; }
    }
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private class DbMax
    {
        public int? max { get; set; }
    }

    [Table(EventsTableName)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private class DbEvent
    {
        [ExplicitKey]
        public int year { get; set; }
        [ExplicitKey]
        public int id { get; set; }
        public string name { get; set; } = "";
        public Guid world_id { get; set; }
    }

    [Table(ChangesTableName)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private class DbChange
    {
        [ExplicitKey]
        public int event_year { get; set; }
        [ExplicitKey]
        public int event_id { get; set; }
        [ExplicitKey]
        public int id { get; set; }
        public string type { get; set; } = "";
        public string? country1_name { get; set; }
        public string? country2_name { get; set; }
        public Guid? area_id { get; set; }
        public string? color { get; set; }
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