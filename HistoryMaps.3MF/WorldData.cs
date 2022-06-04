namespace HistoryMaps;

public struct WorldData
{
    public Guid Id { get; set; }
    public Document Base { get; set; }
    public IEnumerable<Document> Countries { get; set; }
}