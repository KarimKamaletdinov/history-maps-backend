namespace HistoryMaps;

public class AddEventView
{
    private readonly IEventRepository _eventRepository;
    private readonly IWorldBmpRepository _worldBmpRepository;
    private readonly IRootFolderProvider _rootFolderProvider;

    public AddEventView(IEventRepository eventRepository, IWorldBmpRepository worldBmpRepository, 
        IRootFolderProvider rootFolderProvider)
    {
        _eventRepository = eventRepository;
        _worldBmpRepository = worldBmpRepository;
        _rootFolderProvider = rootFolderProvider;
    }

    public void Run()
    {
        Console.WriteLine("Enter year. You can specify ID by adding a '/' as a separator: '<year>/<id>':");
        var line = Console.ReadLine();
        if (line == null)
            throw new NullReferenceException();
        var year = int.Parse(line.Contains('/')
            ? line.Split('/')[0]
            : line);

        int? id = line.Contains('/')
            ? int.Parse(line.Split('/')[1])
            : null;

        Console.WriteLine("Enter name:");
        var name = Console.ReadLine() ?? "";

        var previous = _eventRepository.GetPrevious(year, id);

        var worldId = Guid.NewGuid();
        File.Copy(_rootFolderProvider.GetPath("input", ".bmp"),
            _rootFolderProvider.GetPath("worlds", worldId + ".bmp"));
        File.Copy(_rootFolderProvider.GetPath("input", ".json"),
            _rootFolderProvider.GetPath("worlds", worldId + ".json"));
        Console.WriteLine($"File copied. Id: {worldId}");
        var bw = previous == null ? _worldBmpRepository.GetBaseWorld() : _worldBmpRepository.Get(previous.WorldId);
        var nw = _worldBmpRepository.Get(worldId);
        Console.WriteLine("Worlds loaded");
        var changes = Event.ParseChanges(bw, nw);
        Console.WriteLine("Changes parsed");
        var e = new Event(year, name, changes.ToArray(), bw, nw.Id);
        _eventRepository.Insert(e);
        Console.WriteLine("Changes inserted");
        Console.WriteLine("Finished");
    }
}