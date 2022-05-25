namespace HistoryMaps;

public class Change
{
    public Country Goal { get; set; }
    public bool[,] AddedPoints = new bool[1080, 541];

    public Change(Country goal)
    {
        Goal = goal;
    }

}