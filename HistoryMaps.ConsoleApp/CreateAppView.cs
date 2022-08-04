using System.Text;
using Newtonsoft.Json;

namespace HistoryMaps;

public class CreateAppView
{
    private readonly ICommandHandler<CreateWebApp> _createWebApp;

    public CreateAppView(ICommandHandler<CreateWebApp> createWebApp)
    {
        _createWebApp = createWebApp;
    }

    public void Run()
    {
        _createWebApp.Execute(new ());
    }
}