using System.Drawing;

namespace HistoryMaps;

public class MapArea : Area
{
    public Color Color { get; set; }

    public MapArea(bool[,] points, Color color) : base(points)
    {
        Color = color;
    }

    public MapArea(MapAreaDto dto) : this(dto.Points, dto.Color)
    {

    }

    public override MapAreaDto ToDto()
    {
        return new(Points, Color);
    }

    public override MapArea Copy()
    {
        return new(Points, Color);
    }
}