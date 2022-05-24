using System.Drawing;

namespace HistoryMaps;

public record CountryDto(string Name, bool[,] Points, Color Color):AreaDto(Points, Color);