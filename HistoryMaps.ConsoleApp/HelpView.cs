namespace HistoryMaps;

public class HelpView
{
    public void Run()
    {
        Console.WriteLine("HistoryMaps: admin app");
        Console.WriteLine();
        Console.WriteLine("Avaiable commands:");
        Console.WriteLine("?, h, help: This help text");
        Console.WriteLine("l, load: Load history from database");
        Console.WriteLine("la, load added: Load added events from database");
        Console.WriteLine("a, add: Add new event to database");
        Console.WriteLine("c, create: Create web application and upload to GitHub (NOT FULLY IMPLEMENTED YET)");
    }
}