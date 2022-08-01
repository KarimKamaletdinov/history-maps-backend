namespace HistoryMaps;

public class EventsListPresenter
{
    public event Action<EventDto> ShowEvent; 
    public event Action AddEvent;
    private readonly IEventRepository _repository;

    public EventsListPresenter(IEventRepository repository)
    {
        _repository = repository;
        ShowEvent += _ => { };
        AddEvent += () => { };
    }

    public void Initialize(IEventsListView view)
    {
        var events = _repository.GetAllEventDtos().ToArray();
        view.Events = events;
        view.ShowEvent += ShowEvent;
        view.AddEvent += AddEvent;
    }
}