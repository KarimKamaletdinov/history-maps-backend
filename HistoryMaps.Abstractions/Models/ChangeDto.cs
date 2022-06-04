namespace HistoryMaps;

public record ChangeDto(ChangeType Type, string? Country1Name, string? Country2Name, Guid AreaId);