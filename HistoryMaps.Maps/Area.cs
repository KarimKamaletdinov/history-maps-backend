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

    /// <summary>
    /// Точки [широта, долгота].
    /// Широт 541 = 180 северных + 1 экватор + 180 южных.
    /// Долгот 1080: 360 западных + 360 восточных.
    /// </summary>
    public bool[,] Points = new bool[541, 1080];

    public Area(Color color)
    {
        Color = color;
    }

    public void SetPixel(float latitude, float longitude, bool value)
    {
        Points[(int)(latitude * 2 + 180), (int)(longitude * 2 + 360)] = value;
    }

    public bool GetPixel(float latitude, float longitude)
    {
        return Points[(int)(latitude * 2 + 180), (int)(longitude * 2 + 360)];
    }
}