using System.Drawing;

namespace HistoryMaps;

public static class To3MfConverter
{
    public static (Document, Guid) Convert(WorldDto world)
    {
        var doc = new Document();
        AddArea(doc, world.Water);
        foreach (var country in world.Countries)
        {
            doc.Metadata += $"{country.Name}: {country.Color}";
            AddArea(doc, country);
        }

        var blankArea = new AreaDto(new bool[361, 720], Color.White);
        for (var longitude = 0; longitude < 720; longitude++)
        for (var latitude = 0; latitude < 361; latitude++)
            if(!world.Water.Points[latitude, longitude] && !world.Countries.Any(x => x.Points[latitude, longitude]))
                blankArea.Points[latitude, longitude] = true;
        AddArea(doc, blankArea);
        return (doc, world.Id);
    }

    private static void AddArea(Document doc, AreaDto area)
    {
        var colorId = doc.AddColor(area.Color);
        for (var longitude = 0; longitude < 720; longitude++)
        for (var latitude = 0; latitude < 361; latitude++)
            if (area.Points[latitude, longitude])
                CreateTriangle(doc, latitude, longitude, colorId);
                
    }

    private static void CreateTriangle(Document document, int latitude, int longitude, int colorId)
    {
        if (longitude % 2 == 0)
        {
            CreateTriangle(document,
                CreateVertex(latitude, longitude - 1), 
                CreateVertex(latitude + 1, longitude),
                CreateVertex(latitude, longitude + 1), colorId);
            return;
        }

        CreateTriangle(document,
            CreateVertex(latitude + 1, longitude - 1, false),
            CreateVertex(latitude + 1, longitude + 1, false),
            CreateVertex(latitude, longitude, false), colorId);
    }

    private static void CreateTriangle(Document document, Vertex v1, Vertex v2, Vertex v3, int color)
    {
        var id1 = document.AddVertex(v1);
        var id2 = document.AddVertex(v2);
        var id3 = document.AddVertex(v3);
        document.AddTriangle(new Triangle(id1, id2, id3, color));
    }

    private static Vertex CreateVertex(float latitude, float longitude, bool chet = true)
    {
        var vertex = Matrix.RotateZ((longitude - 360) / 360 * MathF.PI)
            .Multiply(Matrix.RotateY((latitude - 180) / 360 * MathF.PI)
                .Multiply(new Vertex(500, 0, 0)));
        return vertex;
    }
}