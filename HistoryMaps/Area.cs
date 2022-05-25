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

    public bool[,] Points { get; } = new bool[1080, 540];

    public Area(Color color)
    {
        Color = color;
    }

    public AreaDto ToDto()
    {
        return new(Points, Color);
    }
}