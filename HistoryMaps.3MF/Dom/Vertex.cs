using Microsoft.VisualBasic.CompilerServices;

namespace HistoryMaps;

public struct Vertex
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    
    public Vertex(float x, float y, float z) : this()
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vertex RotateX(float angle)
    {
        var vertex = this;
        //vertex.Y *= MathF.Cos(angle);
        //vertex.Z *= MathF.Sin(angle);
        return vertex;
    }

    public Vertex RotateY(float angle)
    {
        var vertex = this;
        vertex.Z = vertex.Z * MathF.Cos(angle) - vertex.X * MathF.Sin(angle);
        vertex.X = vertex.Z * MathF.Sin(angle) + vertex.X * MathF.Cos(angle);
        return vertex;
    }

    public Vertex RotateZ(float angle)
    {
        var vertex = this;
        vertex.X = vertex.X * MathF.Cos(angle) - vertex.Y * MathF.Sin(angle);
        vertex.Y = vertex.X * MathF.Sin(angle) + vertex.Y * MathF.Cos(angle);
        return vertex;
    }

    public static bool operator ==(Vertex left, Vertex right)
    {
        return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
    }

    public static bool operator !=(Vertex left, Vertex right)
    {
        return !(left == right);
    }
}

public struct Matrix
{
    public float[,] Values;

    public Matrix(float[,] values)
    {
        Values = values;
    }

    public Vertex Multiply(Vertex vertex)
    {
        return new Vertex(
            vertex.X * Values[0, 0] + vertex.Y * Values[0, 1] + vertex.Z * Values[0, 2],
            vertex.X * Values[1, 0] + vertex.Y * Values[1, 1] + vertex.Z * Values[1, 2],
            vertex.X * Values[2, 0] + vertex.Y * Values[2, 1] + vertex.Z * Values[2, 2]
        );
    }
    
    public static Matrix RotateX(float f)
    {
        return new Matrix(new [,]
        {
            {1, 0, 0},
            {0, MathF.Cos(f), -MathF.Sin(f)},
            {0, MathF.Sin(f), MathF.Cos(f)}
        });
    }

    public static Matrix RotateY(float f)
    {
        return new Matrix(new [,]
        {
            {MathF.Cos(f), 0, MathF.Sin(f)},
            {0, 1, 0},
            {-MathF.Sin(f), 0, MathF.Cos(f)}
        });
    }

    

    public static Matrix RotateZ(float f)
    {
        return new Matrix(new [,]
        {
            {MathF.Cos(f), -MathF.Sin(f), 0},
            {MathF.Sin(f), MathF.Cos(f), 0},
            {0, 0, 1}
        });
    }
}