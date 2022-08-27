using System.Drawing;

namespace HistoryMaps;

public static class ToVolumeConverter
{
    public static Document Convert(WorldDto world)
    {
        var doc = new Document();
        var arr = new Vertex[Map.Width, Map.Height];
        AddAllVertices(arr);
        AddArea(doc, world.Water, arr);
        foreach (var country in world.Countries)
        {
            doc.Metadata += $"{country.Name}: {country.Color}";
            AddArea(doc, country, arr);
        }

        var blankArea = new MapAreaDto(new bool[Map.Width, Map.Height], Color.White);
        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
                if (!world.Water.Points[x, y] && !world.Countries.Any(c => c.Points[x, y]))
                    blankArea.Points[x, y] = true;
        AddArea(doc, blankArea, arr);
        return doc;
    }

    private static void AddArea(Document doc, MapAreaDto mapArea, Vertex[,] indices)
    {
        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
                if (mapArea.Points[x, y])
                    CreateTriangle(doc, x, y, mapArea.Color, indices);

    }

    private static void CreateTriangle(Document document, int x, int y, Color colorId, Vertex[,] indices)
    {
        if (y % 2 == 0)
        {
            if (x % 2 == 0)
            {
                document.Triangles.Add(new(
                    Index(x - 1, y, indices),
                    Index(x, y + 1, indices),
                    Index(x + 1, y, indices), colorId));
                return;
            }

            document.Triangles.Add(new(
                Index(x - 1, y + 1, indices),
                Index(x + 1, y + 1, indices),
                Index(x, y, indices), colorId));
            return;
        }
        if (x % 2 == 0)
        {
            document.Triangles.Add(new(
                Index(x - 1, y + 1, indices),
                Index(x + 1, y + 1, indices),
                Index(x, y, indices), colorId));
            return;
        }

        document.Triangles.Add(new(
            Index(x - 1, y, indices),
            Index(x, y + 1, indices),
            Index(x + 1, y, indices), colorId));
    }

    private static Vertex Index(int x, int y, Vertex[,] indices)
    {
        if (y < 0)
            y = 0;
        if (x < 0)
            x = Map.Width + x;
        if (y >= Map.Height)
            y = Map.Height - 1;
        if (x >= Map.Width)
            x -= Map.Width;
        return indices[x, y];
    }

    private static Vertex CreateVertex(float y, float x)
    {
        var vertex = Matrix.RotateZ((x - 180) / 180 * MathF.PI)
            .Multiply(Matrix.RotateY((y - 90) / 180 * MathF.PI)
                .Multiply(new(500, 0, 0)));
        return vertex;
    }

    private static void AddAllVertices(Vertex[,] arr)
    {
        for (var x = 0; x < Map.Width; x++)
        {
            for (var y = 0; y < Map.Height; y++)
            {
                arr[x, y] = CreateVertex(y / 3f, x / 3f);
            }
        }
    }
}