namespace HistoryMaps;

/// <summary>
/// Страна
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Name">Название</param>
/// <param name="Bound">Основные точки границы</param>
/// <param name="Color">Цвет, которым она отображается на карте</param>
public record CountryDto(Guid Id, string Name, 
	IReadOnlyCollection<CoordinateDto> Bound, Color Color);