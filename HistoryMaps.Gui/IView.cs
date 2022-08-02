namespace HistoryMaps;

public interface IView
{
    event Action<EventDto> CreateEvent;
    event Action<EventDto> UpdateEvent;
    event Action<EventDto> DeleteEvent;
    event Action<EventDto> EventSelected;
    event Action ReloadHistory;
    event Action LoadAddedHistory;

    IEnumerable<EventDto> Events { set; }
    WorldBitmapDto? CurrentWorld { set; }
}