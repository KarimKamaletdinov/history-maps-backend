namespace HistoryMaps;

public record GetWorld(Guid WorldId) : Query<WorldDto>;