﻿using System.Drawing;

namespace HistoryMaps;

public static class To3MfConverter
{
    public static (Document, Guid) Convert(WorldDto world)
    {
        var doc = new Document();
        var arr = new int[361, 720];
        AddAllVertices(doc, arr);
        AddArea(doc, world.Water, arr);
        foreach (var country in world.Countries)
        {
            doc.Metadata += $"{country.Name}: {country.Color}";
            AddArea(doc, country, arr);
        }

        var blankArea = new AreaDto(new bool[361, 720], Color.White);
        for (var longitude = 0; longitude < 720; longitude++)
            for (var latitude = 0; latitude < 361; latitude++)
                if (!world.Water.Points[latitude, longitude] && !world.Countries.Any(x => x.Points[latitude, longitude]))
                    blankArea.Points[latitude, longitude] = true;
        AddArea(doc, blankArea, arr);
        return (doc, world.Id);
    }

    private static void AddArea(Document doc, AreaDto area, int[,] indices)
    {
        var colorId = doc.AddColor(area.Color);
        for (var longitude = 0; longitude < 720; longitude++)
            for (var latitude = 0; latitude < 361; latitude++)
                if (area.Points[latitude, longitude])
                    CreateTriangle(doc, latitude, longitude, colorId, indices);

    }

    private static void CreateTriangle(Document document, int latitude, int longitude, int colorId, int[,] indices)
    {
        if (longitude % 2 == 0)
        {
            document.Triangles.Add(new Triangle(
                Index(latitude, longitude - 1, indices),
                Index(latitude + 1, longitude, indices),
                Index(latitude, longitude + 1, indices), colorId));
            return;
        }

        document.Triangles.Add(new Triangle(
            Index(latitude + 1, longitude - 1, indices),
            Index(latitude + 1, longitude + 1, indices), 
            Index(latitude, longitude, indices), colorId));
    }

    private static int Index(int latitude, int longitude, int[,] indices)
    {
        if (latitude < 0)
            latitude = 0;
        if (longitude < 0)
            longitude = indices.GetLength(0) + longitude;
        if(latitude >= indices.GetLength(0))
            latitude = indices.GetLength(0) - 1;
        if(longitude >= indices.GetLength(1))
            longitude -= indices.GetLength(1);
        return indices[latitude, longitude];
    }

    //private static void CreateTriangle(Document document, Vertex v1, Vertex v2, Vertex v3, int color)
    //{
    //    var id1 = document.AddVertex(v1);
    //    var id2 = document.AddVertex(v2);
    //    var id3 = document.AddVertex(v3);
    //    document.AddTriangle(new Triangle(id1, id2, id3, color));
    //}

    private static Vertex CreateVertex(float latitude, float longitude)
    {
        var vertex = Matrix.RotateZ((longitude - 360) / 360 * MathF.PI)
            .Multiply(Matrix.RotateY((latitude - 180) / 360 * MathF.PI)
                .Multiply(new Vertex(500, 0, 0)));
        return vertex;
    }

    private static void AddAllVertices(Document doc, int[,] arr)
    {
        var i = 0;
        for (var longitude = 0; longitude < 720; longitude++)
            for (var latitude = 0; latitude < 361; latitude++)
            {
                arr[latitude, longitude] = i;
                doc.Vertices.Add(CreateVertex(latitude, longitude));
                i++;
            }
    }
}