namespace HistoryMaps;

public class ModifyEventPresenter
{
    public event Action ShowEventsListView;
    private readonly IQueryHandler<GetWorld, WorldDto> _getWorldHandler;
    private readonly IEventRepository _eventRepository;
    private readonly IWorldBmpRepository _worldBmpRepository;
    private EventDto? _event;

    public ModifyEventPresenter(IQueryHandler<GetWorld, WorldDto> getWorldHandler,
        IEventRepository eventRepository, IWorldBmpRepository worldBmpRepository)
    {
        ShowEventsListView += () => { };
        _getWorldHandler = getWorldHandler;
        _eventRepository = eventRepository;
        _worldBmpRepository = worldBmpRepository;
    }

    public void Initialize(IModifyEventView view, EventDto e)
    {
        view.World = _worldBmpRepository.GetBitmap(e.WorldId);
        _event = e;
        view.Save += Save;
        view.Back += ShowEventsListView;
    }

    private void Save(WorldBitmapDto world)
    {
        var previous = _eventRepository.GetPrevious(_event!.Year, _event!.Id);
        var pw = previous == null ? _worldBmpRepository.GetBaseWorld() :
            _worldBmpRepository.Get(previous.WorldId);
        var worldId = Guid.NewGuid();
        _worldBmpRepository.InsertBitmap(worldId, world);
        var nw = new World(_getWorldHandler.Execute(new(worldId)));
        var changes = Event.ParseChanges(pw, nw);
        var e = new Event(_event!.Year, _event!.EndYear, _event!.Name, changes.ToArray(), pw, nw.Id);
        _eventRepository.Delete(_event!.Year, _event!.Id);
        _eventRepository.Insert(e);
    }
}