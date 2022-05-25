using System.Drawing;

namespace HistoryMaps;

public record AreaDto(Color Color)
{
    private readonly bool[,] _points = new bool[1080, 541];

    public bool[,] Points
    {
        get => _points;
        init => _points = value.GetLength(0) == 1080 && value.GetLength(1) == 541 ?
            value : throw new DomainException("Points should be bool[1080, 541]");
    }
}