using System.Drawing;

namespace HistoryMaps;

public record CountryDto(string Name, bool[,] Points, Color Color)
    : MapAreaDto(Points, Color);