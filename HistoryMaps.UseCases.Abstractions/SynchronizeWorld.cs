namespace HistoryMaps;

public record SynchronizeWorld(Guid WorldId) : Command;
public record SynchronizeBaseWorld : Command;