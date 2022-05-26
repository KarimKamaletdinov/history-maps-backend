namespace HistoryMaps;

public class ConquestChange : IChange
{
    public Country Goal { get; set; }
    public bool[,] AddedPoints { get; } = new bool[Map.Width, Map.Height];

    public ConquestChange(Country goal)
    {
        Goal = goal;
    }

    public void Apply(World world)
    {
        for (var x = 0; x < Map.Width; x++)
        for (var y = 0; y < Map.Height; y++)
            if (AddedPoints[x, y])
                world.SetPoint(x, y, Goal);
    }

    public ChangeDto ToDto()
    {
        return new();
    }
}