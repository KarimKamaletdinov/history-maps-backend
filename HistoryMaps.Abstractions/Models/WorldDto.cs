namespace HistoryMaps;

public record WorldDto(Guid Id, MapAreaDto Water, IReadOnlyCollection<CountryDto> Countries);