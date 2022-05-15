namespace HistoryMaps;

/// <summary>
/// Страна
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Name">Название</param>
/// <param name="Polygons">Многоугольники, из которых состоит страна</param>
/// <param name="Cutouts">Вырезы в этих многоугольниках (например, в Италии нужен вырез для Ватикана)</param>
/// <param name="Color">Цвет, которым она отображается на карте</param>
public record CountryDto(Guid Id, string Name, 
	IReadOnlyCollection<PolygonDto> Polygons, IReadOnlyCollection<PolygonDto> Cutouts, Color Color);