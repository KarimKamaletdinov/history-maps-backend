namespace HistoryMaps;

public interface IView
{
    event Action UpdateEvents;
    event Action<CreateEventDto> CreateEvent;
    event Action<EventBitmapDto> UpdateEvent;
    event Action<EventDto> DeleteEvent;
    event Action<EventDto> EventSelected;
    event Action ReloadHistory;
    event Action LoadAddedHistory;
    event Action CreateWebApp;

    IEnumerable<EventDto> Events { set; }
    EventBitmapDto? CurrentEvent { set; }
}