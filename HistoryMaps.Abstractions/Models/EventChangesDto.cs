namespace HistoryMaps;

public record EventChangesDto(int Year, int? EndYear, string Name, Guid WorldId, 
    IEnumerable<string> ChangedCountriesNames);