namespace HistoryMaps;

public record LoadAddedHistory : Command
{
    public bool Generate3Mf { get; set; }
}