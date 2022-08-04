using System.Drawing;

namespace HistoryMaps;

public static class To3MfConverter
{
    public static (Document, Guid) Convert(WorldDto world)
    {
        var doc = new Document();
        var arr = new int[Map.Width, Map.Height];
        AddAllVertices(doc, arr);
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
        return (doc, world.Id);
    }

    public static WorldData ConvertSeparately(WorldDto world)
    {
        var baseDocument = new Document();
        var arr = new int[Map.Width, Map.Height];
        AddAllVertices(baseDocument, arr);
        AddArea(baseDocument, world.Water, arr);

        var blankArea = new MapAreaDto(new bool[Map.Width, Map.Height], Color.White);
        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
                if (!world.Water.Points[x, y])
                    blankArea.Points[x, y] = true;
        AddArea(baseDocument, blankArea, arr);

        var countries = new List<Document>();

        foreach (var c in world.Countries)
        {
            var country = new Document();
            var a = new int[Map.Width, Map.Height];
            AddAllVertices(country, a);
            AddArea(country, c, a);
            country.Metadata = c.Name;
            countries.Add(country);
        }

        return new()
        {
            Id = world.Id,
            Base = baseDocument,
            Countries = countries
        };
    }

    private static void AddArea(Document doc, MapAreaDto mapArea, int[,] indices)
    {
        var colorId = doc.AddColor(mapArea.Color);
        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
                if (mapArea.Points[x, y])
                    CreateTriangle(doc, x, y, colorId, indices);

    }

    private static void CreateTriangle(Document document, int x, int y, int colorId, int[,] indices)
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

    private static int Index(int x, int y, int[,] indices)
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

    private static void AddAllVertices(Document doc, int[,] arr)
    {
        var i = 0;
        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
            {
                arr[x, y] = i;
                doc.Vertices.Add(CreateVertex(y / 3f, x / 3f));
                i++;
            }
    }
}