using System.Drawing;

namespace HistoryMaps;

public struct Triangle
{
    public Vertex V1 { get; set; }
    public Vertex V2 { get; set; }
    public Vertex V3 { get; set; }
    public Color Color { get; set; }

    public Triangle(Vertex v1, Vertex v2, Vertex v3, Color color)
    {
        V1 = v1;
        V2 = v2;
        V3 = v3;
        Color = color;
    }
}