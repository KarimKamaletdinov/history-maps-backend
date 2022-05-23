using IxMilia.Stl;

namespace HistoryMaps;

public static class Mapper
{
    public static StlFile Convert(Area area)
    {
        var stlFile = new StlFile();

        for (var longitude = 0; longitude < 720; longitude++)
            for (var latitude = 0; latitude < 361; latitude++)
                if (area.Points[latitude, longitude])
                    stlFile.Triangles.Add(CreateTriangle(latitude, longitude));
        return stlFile;
    }

    private static StlTriangle CreateTriangle(int latitude, int longitude)
    {
        if (latitude % 2 == 0)
        {
            return CreateTriangle(
                CreateVertex(latitude, longitude - 0.5f), 
                CreateVertex(latitude, longitude + 0.5f),
                CreateVertex(latitude + 1, longitude));
        }

        return CreateTriangle(
            CreateVertex(latitude + 1, longitude - 0.5f),
            CreateVertex(latitude + 1, longitude + 0.5f),
            CreateVertex(latitude, longitude));
    }

    private static StlTriangle CreateTriangle(StlVertex v1, StlVertex v2, StlVertex v3)
    {
        var x1 = v1.X;
        var y1 = v1.Y;
        var z1 = v1.Z;

        var x2 = v2.X;
        var y2 = v2.Y;
        var z2 = v2.Z;

        var x3 = v3.X;
        var y3 = v3.Y;
        var z3 = v3.Z;
        return new StlTriangle(
            new StlNormal(
                (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1),
                (z2 - z1) * (x3 - x1) - (x2 - x1) * (z3 - z1),
                (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)),
            v1, v2, v3);
    }


    private static StlVertex CreateVertex(float latitude, float longitude)
    {
        var vertex = new StlVertex(200, 0, 0).RotateZ(latitude / 361 * MathF.PI).RotateY(longitude / 180.5f * MathF.PI);
        return vertex;
    }
}