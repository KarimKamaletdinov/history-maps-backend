namespace HistoryMaps;

public interface IEventsListView
{
    public EventDto[] Events { set; }
    public event Action<EventDto> ShowEvent;
    public event Action AddEvent;
}