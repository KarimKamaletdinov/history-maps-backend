namespace HistoryMaps;

public class EventRepository : IEventRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public EventRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IReadOnlyCollection<Event> GetAllEvents()
    {
        throw new NotImplementedException();
    }
}