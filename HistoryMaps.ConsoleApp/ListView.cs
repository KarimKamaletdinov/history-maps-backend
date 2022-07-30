namespace HistoryMaps;

public class ListView
{
    private readonly IEventRepository _eventRepository;

    public ListView(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public void Run()
    {
        foreach (var e in _eventRepository.GetAllEventDtos())
        {
            Console.Write(e.Year + " ");
            if (e.EndYear != null)
                Console.Write("= " + e.EndYear + " ");
            if (e.Id != 1)
                Console.Write("[" + e.Id + "] ");
            Console.Write(e.Name + "                                      ");
            Console.WriteLine(e.WorldId);
        }
    }
}