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

    //public static bool operator ==(Vertex left, Vertex right)
    //{
    //    return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
    //}

    //public static bool operator !=(Vertex left, Vertex right)
    //{
    //    return !(left == right);
    //}
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
        return new(
            vertex.X * Values[0, 0] + vertex.Y * Values[0, 1] + vertex.Z * Values[0, 2],
            vertex.X * Values[1, 0] + vertex.Y * Values[1, 1] + vertex.Z * Values[1, 2],
            vertex.X * Values[2, 0] + vertex.Y * Values[2, 1] + vertex.Z * Values[2, 2]
        );
    }
    
    public static Matrix RotateX(float f)
    {
        return new(new [,]
        {
            {1, 0, 0},
            {0, MathF.Cos(f), -MathF.Sin(f)},
            {0, MathF.Sin(f), MathF.Cos(f)}
        });
    }

    public static Matrix RotateY(float f)
    {
        return new(new [,]
        {
            {MathF.Cos(f), 0, MathF.Sin(f)},
            {0, 1, 0},
            {-MathF.Sin(f), 0, MathF.Cos(f)}
        });
    }

    

    public static Matrix RotateZ(float f)
    {
        return new(new [,]
        {
            {MathF.Cos(f), -MathF.Sin(f), 0},
            {MathF.Sin(f), MathF.Cos(f), 0},
            {0, 0, 1}
        });
    }
}