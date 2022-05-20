using System.Drawing;

namespace HistoryMaps;

public struct Document
{
    public string Metadata { get; set; }
    public List<Triangle> Triangles;
    public List<Vertex> Vertices;
    public List<Color> Colors;
}