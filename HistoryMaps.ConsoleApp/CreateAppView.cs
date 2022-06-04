using System.Text;
using Newtonsoft.Json;

namespace HistoryMaps;

public class CreateAppView
{
    private readonly IRootFolderProvider _rootFolderProvider;
    private readonly IEventRepository _eventRepository;
    private readonly ICommandHandler<GenerateWorlds> _generateWorlds;
    private readonly ICommandHandler<LoadGitRepo> _loadGitRepo;
    private readonly ICommandHandler<CopyDataToWebApp> _copyDataToWebApp;
    private readonly ICommandHandler<SaveChangesToGitRepo> _saveChangesToGitRepo;

    public CreateAppView(IRootFolderProvider rootFolderProvider, IEventRepository eventRepository, 
        ICommandHandler<GenerateWorlds> generateWorlds, ICommandHandler<LoadGitRepo> loadGitRepo,
        ICommandHandler<CopyDataToWebApp> copyDataToWebApp, ICommandHandler<SaveChangesToGitRepo> saveChangesToGitRepo)
    {
        _rootFolderProvider = rootFolderProvider;
        _eventRepository = eventRepository;
        _generateWorlds = generateWorlds;
        _loadGitRepo = loadGitRepo;
        _copyDataToWebApp = copyDataToWebApp;
        _saveChangesToGitRepo = saveChangesToGitRepo;
    }

    public void Run()
    {
        Console.WriteLine("Start creating a web app");
        Console.WriteLine("Please wait. Loading history and generating .3mf files may take a lot of time");
        _generateWorlds.Execute(new ());
        Console.WriteLine("Loaded history and generated .3mf files");
        File.WriteAllText(_rootFolderProvider.GetPath("events.json"), 
            JsonConvert.SerializeObject(_eventRepository.GetAllEventDtos()),
            Encoding.UTF8);
        Console.WriteLine("Events metadata saved");
        _loadGitRepo.Execute(new ());
        Console.WriteLine("Git repository loaded");
        _copyDataToWebApp.Execute(new ());
        Console.WriteLine("Data copied");
        _saveChangesToGitRepo.Execute(new ());
        Console.WriteLine("Changes saved to Git repository");
        Console.WriteLine("Finished");
    }
}