using HistoryMaps;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Processing;

new WorldRepository(new RootFolderProvider()).Insert(new World(Guid.NewGuid(), new Area(), new []
{
    new Area()
}));