namespace HistoryMaps;

public static class ToVolumeConverter
{
    public static Document Convert(AreaDto area)
    {
        var doc = new Document();
        var arr = new Vertex[Map.Width, Map.Height + 1];
        AddAllVertices(arr);
        AddArea(doc, area, arr);
        return doc;
    }

    private static void AddArea(Document doc, AreaDto mapArea, Vertex[,] indices)
    {
        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
                if (mapArea.Points[x, y])
                    CreateTriangle(doc, x, y, indices);

    }

    private static void CreateTriangle(Document document, int x, int y, Vertex[,] indices)
    {
        document.Triangles.Add(new(
            Index(x, y, indices),
            Index(x, y + 1, indices),
            Index(x + 1, y, indices)));

        document.Triangles.Add(new(
            Index(x + 1, y + 1, indices),
            Index(x, y + 1, indices),
            Index(x + 1, y, indices)));
    }

    private static Vertex Index(int x, int y, Vertex[,] indices)
    {
        if (y < 0)
            y = 0;
        if (x < 0)
            x = Map.Width + x;
        if (y > Map.Height)
            y = Map.Height;
        if (x >= Map.Width)
            x -= Map.Width;
        return indices[x, y];
    }

    private static Vertex CreateVertex(float x, float y)
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
            for (var y = 0; y <= Map.Height; y++)
            {
                arr[x, y] = CreateVertex(x / 3f, y / 3f);
            }
        }
    }
}