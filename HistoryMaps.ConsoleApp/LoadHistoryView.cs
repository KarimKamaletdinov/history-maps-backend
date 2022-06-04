namespace HistoryMaps;

public class LoadHistoryView
{
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;

    public LoadHistoryView(IWorldBmpRepository bmpRepository, IEventRepository eventRepository)
    {
        _bmpRepository = bmpRepository;
        _eventRepository = eventRepository;
    }

    public void Run()
    {
        Console.WriteLine("Start loading history...");
        _bmpRepository.ClearAll();
        Console.WriteLine("Cleared");
        foreach (var ev in _eventRepository.GetAllEvents())
        {
            _bmpRepository.Insert(ev.World);
            Console.WriteLine("Loaded: " + ev.Name);
        }
        Console.WriteLine("Finished");
    }
}