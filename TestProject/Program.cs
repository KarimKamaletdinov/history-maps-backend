
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Processing;

var img = Image.Load("t.bmp");
img.Mutate(x =>x.Fill(Color.White));
img.Mutate(x =>x.DrawLines(Color.Black, 1, new PointF(1, 1), new PointF(1,1)));
img.Save("t.bmp", new BmpEncoder());