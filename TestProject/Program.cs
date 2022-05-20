using System.Drawing;
using System.Runtime.CompilerServices;
using HistoryMaps;

new ThreeMfRepository(new RootFolderProvider()).Insert(new Document
{
    Colors = new List<Color> {Color.Blue},
    Metadata = "",
    Triangles = new List<Triangle>{new (){Color = 0, V1 = 0, V2=1, V3=2}},
    Vertices = new List<Vertex>{new(){X=-1, Y=-1, Z=0}, new(){X=1, Y= 0, Z=0}, new(){X=0, Y=0, Z= 0}}
}, Guid.NewGuid());