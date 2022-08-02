namespace HistoryMaps;

public class Presenter
{
    private readonly IQueryHandler<GetAllEvents, IEnumerable<EventDto>> _getAllEventsHandler;

    public Presenter(IQueryHandler<GetAllEvents, IEnumerable<EventDto>> getAllEventsHandler)
    {
        _getAllEventsHandler = getAllEventsHandler;
    }

    public async Task Initialize(IView view)
    {
        view.Events = await _getAllEventsHandler.Execute(new());
    }
}