namespace HistoryMaps;

public record EventDto(int Year, string Name, Guid WorldId);
public record EventChangesDto(int Year, string Name, Guid WorldId, 
    IEnumerable<string> ChangedCountriesNames);