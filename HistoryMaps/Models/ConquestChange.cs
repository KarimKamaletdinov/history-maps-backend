namespace HistoryMaps;

public class ConquestChange : IChange
{
    public string? ConquerorName { get; set; }
    public string? ConqueredName { get; }

    public Area? ConqueredArea { get; set; }

    public ConquestChange(string? conquerorNameName, string? conqueredNameName, Area? conqueredArea)
    {
        if (conqueredNameName == null && conqueredArea == null)
            throw new DomainException("You must specify at least one of these:" +
                                      $"{nameof(conqueredNameName)}, {nameof(conqueredArea)}");
        ConquerorName = conquerorNameName;
        ConqueredName = conqueredNameName;
        ConqueredArea = conqueredArea;
    }

    public void Apply(World world)
    {
        var conqueror = world.Countries.FirstOrDefault(x => x.Name == ConquerorName);
        var conquered = world.Countries.FirstOrDefault(x => x.Name == ConqueredName);
        if (ConqueredArea == null)
        {
            for (var x = 0; x < Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                    if (conquered != null && conquered.Points[x, y])
                        world.SetPoint(x, y, conqueror);
        }
        else
        {
            for (var x = 0; x < Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                    if (ConqueredArea != null && ConqueredArea.Points[x, y])
                        world.SetPoint(x, y, conqueror);
        }
    }

    public ChangeDto ToDto()
    {
        throw new NotImplementedException();
    }
}