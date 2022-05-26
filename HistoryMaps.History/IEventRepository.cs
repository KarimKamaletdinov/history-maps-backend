namespace HistoryMaps;

public interface IEventRepository
{
    public IReadOnlyCollection<Event> GetAllEvents();
}