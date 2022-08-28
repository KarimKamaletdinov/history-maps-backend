namespace HistoryMaps;

public readonly record struct Matrix
{
    private readonly float[,] _values;

    public Matrix(float[,] values)
    {
        _values = values;
    }

    public Vertex Multiply(Vertex vertex)
    {
        return new(
            vertex.X * _values[0, 0] + vertex.Y * _values[0, 1] + vertex.Z * _values[0, 2],
            vertex.X * _values[1, 0] + vertex.Y * _values[1, 1] + vertex.Z * _values[1, 2],
            vertex.X * _values[2, 0] + vertex.Y * _values[2, 1] + vertex.Z * _values[2, 2],
            vertex.Color
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