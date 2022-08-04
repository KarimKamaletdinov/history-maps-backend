namespace HistoryMaps;

public record LoadHistory : Command
{
    public bool Generate3Mf { get; set; }
}
