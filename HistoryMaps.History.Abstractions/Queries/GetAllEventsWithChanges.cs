namespace HistoryMaps;

public record GetAllEventsWithChanges : Query<IEnumerable<EventChangesDto>>;