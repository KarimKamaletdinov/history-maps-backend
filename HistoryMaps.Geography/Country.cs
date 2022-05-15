namespace HistoryMaps;

public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Color Color { get; set; }
    public List<Polygon> Polygons { get; set; }
    public List<Polygon> Cutouts { get; set; }

    public Country(Guid id, string name, Color color,
        IEnumerable<Polygon> polygons, IEnumerable<Polygon> cutouts)
    {
        Id = id;
        Name = name;
        Color = color;
        Polygons = polygons.ToList();
        Cutouts = cutouts.ToList();
    }

    public Country(CountryDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        Color = dto.Color;
        Polygons = dto.Polygons.Select(x => new Polygon(x)).ToList();
        Cutouts = dto.Cutouts.Select(x => new Polygon(x)).ToList();
    }
}
