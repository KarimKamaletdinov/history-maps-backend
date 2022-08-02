namespace HistoryMaps;

public record GetAllEventsWithChanges : Query<IEnumerable<EventChangesDto>>;
public record GetAllEvents : Query<IEnumerable<EventDto>>;