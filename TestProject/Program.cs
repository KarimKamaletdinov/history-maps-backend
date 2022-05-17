using System.Drawing;
using HistoryMaps;

var r = new WorldRepository(new RootFolderProvider());
var water = new Area("water", Color.Blue);
water.Points[1, 1] = true;
r.Insert(new World(Guid.NewGuid(), water, Array.Empty<Area>()));