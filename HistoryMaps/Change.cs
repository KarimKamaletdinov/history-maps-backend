namespace HistoryMaps;

public class Change
{
    public Country Goal { get; set; }
    public bool[,] AddedPoints = new bool[Map.Width, Map.Height];

    public Change(Country goal)
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
}