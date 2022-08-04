namespace HistoryMaps;

public interface IEventRepository
{
    void Insert(Event e);
    IReadOnlyCollection<Event> GetAllEvents();
    IReadOnlyCollection<EventDto> GetAllEventDtos();
    EventDto? GetPrevious(int year, int? id);
    void Delete(int year, int id);
    EventDto Get(int year, int id);
    int GenerateId(int year);
}