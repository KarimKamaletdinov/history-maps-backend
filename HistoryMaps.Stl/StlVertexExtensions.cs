using IxMilia.Stl;

namespace HistoryMaps;

internal static class StlVertexExtensions
{
    public static StlVertex RotateX(this StlVertex v, float angle)
    {
        var vertex = v;
        vertex.Y *= MathF.Cos(angle);
        vertex.Z *= MathF.Sin(angle);
        return vertex;
    }

    public static StlVertex RotateY(this StlVertex v, float angle)
    {
        var vertex = v;
        vertex.Z = vertex.Z * MathF.Cos(angle) - vertex.X * MathF.Sin(angle);
        vertex.X = vertex.Z * MathF.Sin(angle) + vertex.X * MathF.Cos(angle);
        return v;
    }

    public static StlVertex RotateZ(this StlVertex v, float angle)
    {
        var vertex = v;
        vertex.X = vertex.X * MathF.Cos(angle) - vertex.Y * MathF.Sin(angle);
        vertex.Y = vertex.X * MathF.Sin(angle) + vertex.Y * MathF.Cos(angle);
        return vertex;
    }
}