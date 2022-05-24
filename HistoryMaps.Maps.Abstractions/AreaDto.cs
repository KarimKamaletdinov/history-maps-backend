using System.Drawing;

namespace HistoryMaps;

/// <summary>
/// Область на карте
/// </summary>
/// <param name="Points">
/// Точки [широта, долгота].
/// Широт 541 = 90 северных + 1 экватор + 90 южных.
/// Долгот 1080: 180 западных + 1 экватор + 180 восточных.
/// </param>
public record AreaDto(bool[,] Points, Color Color);

public record CountryDto(string Name, bool[,] Points, Color Color):AreaDto(Points, Color);