using System.Drawing;

namespace HistoryMaps;

public static class Xml
{
    public static string Convert(Triangle triangle)
    {
        return $"<triangle v1=\"{triangle.V1}\" v2=\"{triangle.V2}\" v3=\"{triangle.V3}\"" +
               $" pid=\"1\" p1=\"{triangle.Color}\" />";
    }

    
    public static string Convert(IEnumerable<Triangle> triangles)
    {
        return string.Join("\r\n", triangles.Select(Convert)) + "\r\n";
    }

    public static string Convert(Vertex vertex)
    {
        return $"<vertex x=\"{vertex.X}\" y=\"{vertex.Y}\" z=\"{vertex.Z}\" />";
    }

    public static string Convert(IEnumerable<Vertex> vertices)
    {
        return string.Join("\r\n", vertices.Select(Convert)) + "\r\n";
    }

    public static string Convert(Color color)
    {
        return $"<base name=\"Material\" displaycolor=\"{ToHex(color)}\" />";
    }

    public static string Convert(IEnumerable<Color> colors)
    {
        return string.Join("\r\n", colors.Select(Convert)) + "\r\n";
    }

    private static string ToHex(Color c)
    {
        return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
}