namespace HistoryMaps;

public class Application
{
    private readonly LoadHistoryView _loadHistoryView;
    private readonly AddEventView _addEventView;
    private readonly CreateAppView _createAppView;
    private readonly HelpView _helpView;
    private readonly InvalidCommandView _invalidCommandView;

    public Application(LoadHistoryView loadHistoryView, AddEventView addEventView, CreateAppView createAppView,
        HelpView helpView, InvalidCommandView invalidCommandView)
    {
        _loadHistoryView = loadHistoryView;
        _addEventView = addEventView;
        _createAppView = createAppView;
        _helpView = helpView;
        _invalidCommandView = invalidCommandView;
    }

    public void Run()
    {
        while (true)
        {
            var cmd = Console.ReadLine() ?? "";

            switch (cmd)
            {
                case "l":
                case "load":
                    _loadHistoryView.Run();
                    break;
                case "a":
                case "add":
                    _addEventView.Run();
                    break;
                case "c":
                case "create":
                    _createAppView.Run();
                    break;
                case "?":
                case "h":
                case "help":
                    _helpView.Run();
                    break;
                default:
                    _invalidCommandView.Run(cmd);
                    break;
            }
            Console.WriteLine();
        }
    }
}