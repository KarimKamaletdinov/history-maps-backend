using System.Drawing;

namespace HistoryMaps;

public record AreaDto(Color Color)
{
    private readonly bool[,] _points = new bool[Map.Width, Map.Height];

    public bool[,] Points
    {
        get => _points;
        init => _points = value.GetLength(0) == Map.Width && value.GetLength(1) == Map.Height ?
            value : throw new DomainException("Points should be bool[Map.Width, Map.Height]");
    }
}