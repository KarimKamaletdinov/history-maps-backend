using System.Drawing;

namespace HistoryMaps;

public class CreateCountryChange : IChange
{
    public string NewCountryName { get; }
    public Color NewCountryColor { get; }

    public CreateCountryChange(string newCountryName, Color newCountryColor)
    {
        NewCountryName = newCountryName;
        NewCountryColor = newCountryColor;
    }

    public void Apply(World world)
    {
        world.Countries.Add(new(new bool[Map.Width, Map.Height], NewCountryName, NewCountryColor));
    }

    public IEnumerable<string> GetChangedCountries(World baseWorld)
    {
        yield return NewCountryName;
    }
}