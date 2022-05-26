using CommandLine;

namespace HistoryMaps;

[Verb("sync")]
public class SyncWorld
{
    [Option("id", Required = true)]
    public string WorldId { get; set; } = "";
}