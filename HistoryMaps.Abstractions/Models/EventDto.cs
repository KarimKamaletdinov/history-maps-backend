namespace HistoryMaps;

public record EventDto(int Year, int Id, int? EndYear, string Name, Guid WorldId);
public record CreateEventDto(int Year, int? EndYear, string Name, Guid WorldId);