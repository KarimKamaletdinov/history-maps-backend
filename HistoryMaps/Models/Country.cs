using System.Drawing;

namespace HistoryMaps;

public class Country : MapArea
{
    public string Name { get; set; }

    public Country(bool[,] points, string name, Color color) : base(points, color)
    {
        Name = name;
    }

    public Country(CountryDto dto) : this(dto.Points, dto.Name, dto.Color)
    {
    }

    public override CountryDto ToDto()
    {
        return new(Name, Points, Color);
    }

    public override Country Copy()
    {
        return new(Points, Name, Color);
    }
}