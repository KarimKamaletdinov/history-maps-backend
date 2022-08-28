using System.Drawing;

namespace HistoryMaps;

public struct Triangle
{
    public int V1 { get; set; }
    public int V2 { get; set; }
    public int V3 { get; set; }

    public Triangle(int v1, int v2, int v3)
    {
        V1 = v1;
        V2 = v2;
        V3 = v3;
    }
}