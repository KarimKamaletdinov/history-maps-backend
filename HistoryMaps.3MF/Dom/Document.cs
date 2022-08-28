using System.Drawing;
using System.IO.Compression;

namespace HistoryMaps;

public struct Document
{
    public List<Triangle> Triangles = new ();
    public List<Vertex> Vertices = new ();

    public Document()
    {
    }
}