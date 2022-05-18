using System.Drawing;
using HistoryMaps;

var r = new WorldBmpRepository(new RootFolderProvider());
//var world = new World(Guid.NewGuid(), new Area(Color.Blue), Array.Empty<Country>());
//world.Water.SetPixel(0,0, true);
//r.Insert(world);
var w = r.Get(Guid.Parse("96d68a4e-f97f-4823-89c9-b34f93020c5e"), new Dictionary<string, Color>{{"water", Color.Black}});
for (var i0 = 0; i0 < w.Water.Points.GetLength(0); i0++)
for (var i1 = 0; i1 < w.Water.Points.GetLength(1); i1++)
{
    var point = w.Water.Points[i0, i1];
    Console.WriteLine($"{i0}, {i1}: {(point ? "water" : "dry")}");
}