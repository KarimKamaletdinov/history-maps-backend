using System.Drawing;

namespace HistoryMaps;

public class Country : Area
{
    public string Name { get; set; }

    public Country(string name, Color color) : base(color)
    {
        Name = name;
    }

    public new CountryDto ToDto()
    {
        return new(Name, Color) { Points = Points };
    }
}