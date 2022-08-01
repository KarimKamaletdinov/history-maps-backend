namespace HistoryMaps;

public class AddEventPresenter
{
    public event Action ShowEventsListView;
    public event Action<EventDto> ShowModifyEventView;
    private readonly IEventRepository _eventRepository;
    private readonly IWorldBmpRepository _worldBmpRepository;

    public AddEventPresenter(IQueryHandler<GetWorld, WorldDto> getWorldHandler,
        IEventRepository eventRepository, IWorldBmpRepository worldBmpRepository)
    {
        ShowEventsListView += () => { };
        ShowModifyEventView += _ => { };
        _eventRepository = eventRepository;
        _worldBmpRepository = worldBmpRepository;
    }

    public void Initialize(IAddEventView view)
    {
        view.Save += Save;
        view.Cancel += ShowEventsListView;
    }

    private void Save(int year, int? endYear, string name)
    {
        var id = _eventRepository.GenerateId(year);
        var previous = _eventRepository.GetPrevious(year, id);
        var pw = previous == null
            ? _worldBmpRepository.GetBaseWorld()
            : _worldBmpRepository.Get(previous.WorldId);
        var e = new Event(year, endYear, name, Array.Empty<IChange>(), pw, Guid.NewGuid());
        _eventRepository.Insert(e);
        ShowModifyEventView(new(e.Year, id, e.EndYear, e.Name, e.WorldId));
    }
}