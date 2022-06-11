namespace HistoryMaps;

public interface IEventRepository
{
    void Insert(Event e);
    IReadOnlyCollection<Event> GetAllEvents();
    EventDto? GetPrevious(int year, int? id);
}