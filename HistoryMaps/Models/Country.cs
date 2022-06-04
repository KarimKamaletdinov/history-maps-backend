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
        return new((bool[,])Points.Clone(), Name, Color);
    }

    public static bool operator ==(Country? c1, Country? c2)
    {
        if (c1 is null && c2 is null)
            return true;
        if (c1 is null || c2 is null)
            return false;
        return c1.Name == c2.Name;
    }

    public static bool operator !=(Country? c1, Country? c2)
    {
        return !(c1 == c2);
    }
    protected bool Equals(Country other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Country)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}