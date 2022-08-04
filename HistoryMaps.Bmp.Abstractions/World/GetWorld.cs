namespace HistoryMaps;

public record GetWorld(Guid WorldId) : Query<WorldDto>;
public record GetWorldBitmap(Guid WorldId) : Query<WorldBitmapDto>;