using System.Drawing;

namespace HistoryMaps;

public record MapAreaDto(bool[,] Points, Color Color) : AreaDto(Points);