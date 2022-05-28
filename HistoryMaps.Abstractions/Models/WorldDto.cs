﻿namespace HistoryMaps;

/// <summary>
/// Мир размером Map.WidthxMap.Height пиксель
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Water">Область, занимаемая водой</param>
/// <param name="Countries">Области, занимаемые странами</param>
public record WorldDto(Guid Id, MapAreaDto Water, IReadOnlyCollection<CountryDto> Countries);