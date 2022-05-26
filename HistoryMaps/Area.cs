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

    public AreaDto ToDto()
    {
        return new(Color){Points = Points};
    }
}