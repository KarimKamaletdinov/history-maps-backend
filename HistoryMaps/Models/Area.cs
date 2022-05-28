namespace HistoryMaps;

public class Area
{
    private readonly bool[,] _points = new bool[Map.Width, Map.Height];

    public bool[,] Points
    {
        get => _points;
        init
        {
            if (value.GetLength(0) != Map.Width || value.GetLength(1) != Map.Height)
                throw new ValidationException("Area.Points", value,
                    $"point[{Map.Width}, {Map.Height}]");
            _points = value;
        }
    }

    public Area(bool[,] points)
    {
        Points = points;
    }

    public Area(AreaDto dto) : this(dto.Points)
    {

    }

    public virtual AreaDto ToDto()
    {
        return new(Points);
    }

    public virtual Area Copy()
    {
        return new(Points);
    }
}