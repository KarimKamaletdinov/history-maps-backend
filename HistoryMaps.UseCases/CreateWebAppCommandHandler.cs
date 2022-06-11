using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HistoryMaps;

public class CreateWebAppCommandHandler : ICommandHandler<CreateWebApp>
{
    private readonly ILogger<Create3DWorldCommandHandler> _logger;
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IWorld3MfRepository _tMfRepository;
    private readonly ICommandHandler<SynchronizeWorld> _synchronizer;
    private readonly IRootFolderProvider _rootFolderProvider;
    private readonly ICommandHandler<LoadGitRepo> _loadGitRepo;
    private readonly ICommandHandler<CopyDataToWebApp> _copyDataToWebApp;
    private readonly ICommandHandler<SaveChangesToGitRepo> _saveChangesToGitRepo;

    public CreateWebAppCommandHandler(ILogger<Create3DWorldCommandHandler> logger, IWorldBmpRepository bmpRepository, IEventRepository eventRepository, IWorld3MfRepository tMfRepository, ICommandHandler<SynchronizeWorld> synchronizer, IRootFolderProvider rootFolderProvider, ICommandHandler<LoadGitRepo> loadGitRepo, ICommandHandler<CopyDataToWebApp> copyDataToWebApp, ICommandHandler<SaveChangesToGitRepo> saveChangesToGitRepo)
    {
        _logger = logger;
        _bmpRepository = bmpRepository;
        _eventRepository = eventRepository;
        _tMfRepository = tMfRepository;
        _synchronizer = synchronizer;
        _rootFolderProvider = rootFolderProvider;
        _loadGitRepo = loadGitRepo;
        _copyDataToWebApp = copyDataToWebApp;
        _saveChangesToGitRepo = saveChangesToGitRepo;
    }

    public void Execute(CreateWebApp command)
    {
        var dtos = new List<EventChangesDto>();
        _logger.LogInformation("Start creating a web app...");
        _logger.LogInformation("Start loading history");
        _bmpRepository.ClearAll();
        _tMfRepository.ClearAll();
        _logger.LogInformation("Cleared");
        var events = _eventRepository.GetAllEvents();
        foreach (var e in events)
        {
            _bmpRepository.Insert(e.World);
            _synchronizer.Execute(new (e.World.Id));
            _logger.LogInformation("Loaded {name}", e.Name);
            dtos.Add(e.ToDto());
        }
        _logger.LogInformation("History loaded");
        File.WriteAllText(_rootFolderProvider.GetPath("events.json"), 
            JsonConvert.SerializeObject(dtos),
            Encoding.UTF8);
        _logger.LogInformation("Events metadata saved");
        _loadGitRepo.Execute(new ());
        Console.WriteLine("Git repository loaded");
        _copyDataToWebApp.Execute(new ());
        Console.WriteLine("Data copied");
        _saveChangesToGitRepo.Execute(new ());
        Console.WriteLine("Changes saved to Git repository");
        _logger.LogInformation("Finished");
    }
}