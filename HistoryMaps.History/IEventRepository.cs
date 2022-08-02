namespace HistoryMaps;

public interface IEventRepository
{
    Task Insert(Event e);
    Task<IReadOnlyCollection<Event>> GetAllEvents();
    Task<IReadOnlyCollection<EventDto>> GetAllEventDtos();
    Task<EventDto?> GetPrevious(int year, int? id);
    Task Delete(int year, int id);
    Task<EventDto> Get(int year, int id);
    Task<int> GenerateId(int year);
}