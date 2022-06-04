namespace HistoryMaps;

public record GetAllEvents() : Query<IEnumerable<EventDto>>;