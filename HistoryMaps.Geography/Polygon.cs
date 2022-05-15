namespace HistoryMaps;

public class Polygon
{
    public List<Coordinate> Bound { get; set; }

    public Polygon(IEnumerable<Coordinate> bound)
    {
        Bound = bound.ToList();
    }

    public Polygon(PolygonDto dto)
    {
        Bound = dto.Bound.Select(x => new Coordinate(x)).ToList();
    }
}