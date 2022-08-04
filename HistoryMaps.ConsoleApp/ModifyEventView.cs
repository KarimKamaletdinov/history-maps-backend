namespace HistoryMaps;

public class ModifyEventView
{
    private readonly IEventRepository _eventRepository;
    private readonly IWorldBmpRepository _worldBmpRepository;
    private readonly IRootFolderProvider _rootFolderProvider;

    public ModifyEventView(IEventRepository eventRepository, IWorldBmpRepository worldBmpRepository, 
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
        var yearLine = line.Contains('/')
            ? line.Split('/')[0]
            : line;

        var year = int.Parse(yearLine.Contains('=')
            ? yearLine.Split('=')[0]
            : yearLine);

        int? endYear = yearLine.Contains('=')
            ? int.Parse(yearLine.Split('=')[1])
            : null;

        var id = line.Contains('/')
            ? int.Parse(line.Split('/')[1])
            : 1;

        Console.WriteLine("Enter name:");
        var name = Console.ReadLine() ?? "";

        var previous = _eventRepository.GetPrevious(year, id);
        var current = _eventRepository.Get(year, id);

        File.Copy(_rootFolderProvider.GetPath("worlds", current.WorldId + ".bmp"),
            _rootFolderProvider.GetPath("input", ".bmp"), true);

        File.Copy(_rootFolderProvider.GetPath("worlds", current.WorldId + ".json"),
            _rootFolderProvider.GetPath("input", ".json"), true);

        Console.WriteLine("Modify file in the 'input' folder and then press Enter " +
                          "to continue");
        Console.ReadLine();

        var worldId = Guid.NewGuid();
        var inputFiles = Directory.GetFiles(_rootFolderProvider.GetPath("input"));
        File.Copy(inputFiles.First(x => x.EndsWith(".bmp")),
            _rootFolderProvider.GetPath("worlds", worldId + ".bmp"));
        File.Copy(inputFiles.First(x => x.EndsWith(".json")),
            _rootFolderProvider.GetPath("worlds", worldId + ".json"));
        Console.WriteLine($"File copied. Id: {worldId}");
        var bw = previous == null ? _worldBmpRepository.GetBaseWorld() : _worldBmpRepository.Get(previous.WorldId);
        var nw = _worldBmpRepository.Get(worldId);
        Console.WriteLine("Worlds loaded");
        var changes = Event.ParseChanges(bw, nw);
        Console.WriteLine("Changes parsed");
        var e = new Event(year, endYear, name, changes.ToArray(), bw, nw.Id);
        _eventRepository.Delete(year, id);
        _eventRepository.Insert(e);
        Console.WriteLine("Changes inserted");
        Console.WriteLine("Finished");
    }
}