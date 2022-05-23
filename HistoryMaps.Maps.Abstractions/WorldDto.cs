namespace HistoryMaps;

/// <summary>
/// Мир размером 720x361 пиксель
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Water">Область, занимаемая водой</param>
/// <param name="Countries">Области, занимаемые странами</param>
public record WorldDto(Guid Id, AreaDto Water, IReadOnlyCollection<CountryDto> Countries);