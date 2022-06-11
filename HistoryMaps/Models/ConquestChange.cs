namespace HistoryMaps;

public class ConquestChange : IChange
{
    public string? ConquerorName { get; set; }

    public Area ConqueredArea { get; set; }

    public ConquestChange(string? conquerorNameName, Area conqueredArea)
    {
        ConquerorName = conquerorNameName;
        ConqueredArea = conqueredArea;
    }

    public void Apply(World world)
    {
        var conqueror = world.Countries.FirstOrDefault(x => x.Name == ConquerorName);
        for (var x = 0; x < Map.Width; x++)
            for (var y = 0; y < Map.Height; y++)
                if (ConqueredArea.Points[x, y])
                    world.SetPoint(x, y, conqueror);
    }

    public IEnumerable<string> GetChangedCountries(World baseWorld)
    {
        if (ConquerorName != null)
            yield return ConquerorName;
        for (var x = 0; x < Map.Width; x++)
        for (var y = 0; y < Map.Height; y++)
            if (ConqueredArea.Points[x, y])
                foreach (var country in baseWorld.Countries.Where(country => country.Points[x, y]))
                    yield return country.Name;
    }
}