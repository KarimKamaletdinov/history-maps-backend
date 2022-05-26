using System.Drawing;

namespace HistoryMaps;

/// <summary>
/// Область на карте
/// </summary>
public class Area
{
    /// <summary>
    /// Цвет области
    /// </summary>
    public Color Color { get; set; }

    public bool[,] Points { get; } = new bool[Map.Width, Map.Height];

    public Area(Color color)
    {
        Color = color;
    }

    public Area(AreaDto dto)
    {
        if (dto.Points.GetLength(0) != Map.Width &&
            dto.Points.GetLength(1) != Map.Height)
            throw new ValidationException("Area.Points", dto.Points,
                $"point[{Map.Width}, {Map.Height}]");
        Color = dto.Color;
        Points = dto.Points;
    }

    public AreaDto ToDto()
    {
        return new(Color){Points = Points};
    }
}