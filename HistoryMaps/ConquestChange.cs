namespace HistoryMaps;

public class ConquestChange : IChange
{
    public Country Goal { get; set; }
    public Area AddedArea { get; set; }

    public ConquestChange(Country goal, Area addedArea)
    {
        Goal = goal;
        AddedArea = addedArea;
    }

    public void Apply(World world)
    {
        for (var x = 0; x < Map.Width; x++)
        for (var y = 0; y < Map.Height; y++)
            if (AddedArea.Points[x, y])
                world.SetPoint(x, y, Goal);
    }

    public ChangeDto ToDto()
    {
        return new();
    }
}