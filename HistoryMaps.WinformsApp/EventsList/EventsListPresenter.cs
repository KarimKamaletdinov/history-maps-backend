namespace HistoryMaps;

public class EventsListPresenter
{
    public event Action<EventDto> ShowEvent; 
    private readonly IEventRepository _repository;

    public EventsListPresenter(IEventRepository repository)
    {
        _repository = repository;
        ShowEvent += _ => { };
    }

    public void Initialize(IEventsListView view)
    {
        var events = _repository.GetAllEventDtos().ToArray();
        view.Events = events;
        view.ShowEvent += ShowEvent;
    }
}