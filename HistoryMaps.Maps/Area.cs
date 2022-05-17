using System.Drawing;

namespace HistoryMaps;

/// <summary>
/// Область на карте
/// </summary>
public class Area
{
    /// <summary>
    /// Название области
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цвет области
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Точки [широта, долгота].
    /// Широт 181 = 90 северных + 1 экватор + 90 южных.
    /// Долгот 361: 180 западных + 1 экватор + 180 восточных.
    /// </summary>
    public bool[,] Points = new bool[181, 361];

    public Area(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    public void SetPixel(int latitude, int longitude, bool value)
    {
        Points[latitude + 90, longitude + 180] = value;
    }

    public bool GetPixel(int latitude, int longitude)
    {
        return Points[latitude + 90, longitude + 180];
    }
}