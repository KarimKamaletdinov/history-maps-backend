using System.Drawing;
using System.IO.Compression;

namespace HistoryMaps;

public struct Document
{
    public string Metadata { get; set; } = "";
    public List<Triangle> Triangles = new ();

    public Document()
    {
    }
}