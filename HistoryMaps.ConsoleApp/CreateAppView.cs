using System.Text;
using Newtonsoft.Json;

namespace HistoryMaps;

public class CreateAppView
{
    private readonly ICommandHandler<GenerateWorlds> _generateWorlds;
    private readonly IRootFolderProvider _rootFolderProvider;
    private readonly IEventRepository _eventRepository;

    public CreateAppView(ICommandHandler<GenerateWorlds> generateWorlds, IRootFolderProvider rootFolderProvider, IEventRepository eventRepository)
    {
        _generateWorlds = generateWorlds;
        _rootFolderProvider = rootFolderProvider;
        _eventRepository = eventRepository;
    }

    public void Run()
    {
        Console.WriteLine("Start creating a web app");
        Console.WriteLine("Creating a web app is not implemented yet");
        _generateWorlds.Execute(new ());
        Console.WriteLine("Loaded history and generated .3MF files");
        File.WriteAllText(_rootFolderProvider.GetPath("events.json"), 
            JsonConvert.SerializeObject(_eventRepository.GetAllEventDtos()),
            Encoding.UTF8);
        Console.WriteLine("Events metadata saved");
        Console.WriteLine("Finished");
    }
}