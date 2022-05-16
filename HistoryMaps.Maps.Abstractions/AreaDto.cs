namespace HistoryMaps;

/// <summary>
/// Область на карте
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Name">Название области</param>
/// <param name="Points">
/// Точки [широта, долгота].
/// Широт 181 = 90 северных + 1 экватор + 90 южных.
/// Долгот 361: 180 западных + 1 экватор + 180 восточных.
/// </param>
public record AreaDto(Guid Id, string Name, bool[][] Points);