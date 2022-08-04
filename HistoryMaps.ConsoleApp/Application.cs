namespace HistoryMaps;

public class Application
{
    private readonly LoadHistoryView _loadHistoryView;
    private readonly AddEventView _addEventView;
    private readonly CreateAppView _createAppView;
    private readonly HelpView _helpView;
    private readonly InvalidCommandView _invalidCommandView;
    private readonly LoadAddedHistoryView _loadAddedHistoryView;
    private readonly ModifyEventView _modifyEventView;
    private readonly ListView _listView;

    public Application(LoadHistoryView loadHistoryView, AddEventView addEventView, CreateAppView createAppView,
        HelpView helpView, InvalidCommandView invalidCommandView, LoadAddedHistoryView loadAddedHistoryView, ModifyEventView modifyEventView, ListView listView)
    {
        _loadHistoryView = loadHistoryView;
        _addEventView = addEventView;
        _createAppView = createAppView;
        _helpView = helpView;
        _invalidCommandView = invalidCommandView;
        _loadAddedHistoryView = loadAddedHistoryView;
        _modifyEventView = modifyEventView;
        _listView = listView;
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
                case "la":
                case "load added":
                    _loadAddedHistoryView.Run();
                    break;
                case "m":
                case "mod":
                case "modify":
                    _modifyEventView.Run();
                    break;
                case "li":
                case "list":
                    _listView.Run();
                    break;
                default:
                    _invalidCommandView.Run(cmd);
                    break;
            }
            Console.WriteLine();
        }
    }
}