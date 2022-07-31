namespace HistoryMaps;

public class ModifyEventPresenter
{
    public event Action ShowEventsListView;
    private readonly IQueryHandler<GetWorld, WorldDto> _getWorldHandler;
    private readonly IEventRepository _eventRepository;
    private EventDto? _event;

    public ModifyEventPresenter(IQueryHandler<GetWorld, WorldDto> getWorldHandler,
        IEventRepository eventRepository)
    {
        ShowEventsListView += () => { };
        _getWorldHandler = getWorldHandler;
        _eventRepository = eventRepository;
    }

    public void Initialize(IModifyEventView view, EventDto e)
    {
        view.World = _getWorldHandler.Execute(new(e.WorldId));
        _event = e;
        view.Save += Save;
        view.Back += ShowEventsListView;
    }

    private void Save(Guid worldId)
    {
        var pw = new World(_getWorldHandler.Execute(new(
            _eventRepository.GetPrevious(_event!.Year, _event!.Id)!.WorldId)));
        var nw = new World(_getWorldHandler.Execute(new(worldId)));
        var changes = Event.ParseChanges(pw, nw);
        var e = new Event(_event!.Year, _event!.EndYear, _event!.Name, changes.ToArray(), pw, nw.Id);
        _eventRepository.Delete(_event!.Year, _event!.Id);
        _eventRepository.Insert(e);
    }
}