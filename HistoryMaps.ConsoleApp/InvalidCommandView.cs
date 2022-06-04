namespace HistoryMaps;

public class InvalidCommandView
{
    public void Run(string cmd)
    {
        Console.WriteLine($"Invalid command: {cmd}");
        Console.WriteLine("Type 'help' for help");
    }
}