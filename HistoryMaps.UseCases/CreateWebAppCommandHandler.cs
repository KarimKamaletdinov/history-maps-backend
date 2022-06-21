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
        var generatedBmp = _bmpRepository.GetAllIds().ToArray();
        var generated3Mf = _tMfRepository.GetAllIds().ToArray();
        _logger.LogInformation("Cleared");
        var events = _eventRepository.GetAllEvents();
        foreach (var e in events)
        {
            if(!generatedBmp.Contains(e.WorldId))
                _bmpRepository.Insert(e.World);
            if(!generated3Mf.Contains(e.WorldId))
                _synchronizer.Execute(new (e.WorldId));
            dtos.Add(e.ToDto());
            _logger.LogInformation("Loaded {name}", e.Name);
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