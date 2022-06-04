using System.Drawing;
using System.IO.Compression;

namespace HistoryMaps;

public struct Document
{
    public string Metadata { get; set; } = "";
    public List<Triangle> Triangles = new ();
    public List<Vertex> Vertices = new ();
    public List<Color> Colors = new ();

    public Document()
    {
    }

    public int AddVertex(Vertex vertex)
    {
        //for (int i = 0; i < Vertices.Count; i++)
        //{
        //    if (Vertices[i] == vertex)
        //    {
        //        return i;
        //    }
        //}
        Vertices.Add(vertex);
        return Vertices.Count - 1;
    }

    public int AddTriangle(Triangle triangle)
    {
        Triangles.Add(triangle);
        return Triangles.Count - 1;
    }

    public int AddColor(Color color)
    {
        Colors.Add(color);
        return Colors.Count - 1;
    }
}