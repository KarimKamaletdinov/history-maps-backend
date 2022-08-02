using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HistoryMaps;

public class CreateWebAppCommandHandler : ICommandHandler<CreateWebApp>
{
    private readonly ILogger<CreateWebAppCommandHandler> _logger;
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IWorld3MfRepository _tMfRepository;
    private readonly ICommandHandler<SynchronizeWorld> _synchronizer;
    private readonly IRootFolderProvider _rootFolderProvider;
    private readonly ICommandHandler<LoadGitRepo> _loadGitRepo;
    private readonly ICommandHandler<CopyDataToWebApp> _copyDataToWebApp;
    private readonly ICommandHandler<SaveChangesToGitRepo> _saveChangesToGitRepo;

    public CreateWebAppCommandHandler(ILogger<CreateWebAppCommandHandler> logger, IWorldBmpRepository bmpRepository, IEventRepository eventRepository, IWorld3MfRepository tMfRepository, ICommandHandler<SynchronizeWorld> synchronizer, IRootFolderProvider rootFolderProvider, ICommandHandler<LoadGitRepo> loadGitRepo, ICommandHandler<CopyDataToWebApp> copyDataToWebApp, ICommandHandler<SaveChangesToGitRepo> saveChangesToGitRepo)
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

    public async Task Execute(CreateWebApp command)
    {
        var dtos = new List<EventChangesDto>();
        _logger.LogInformation("Start creating a web app...");
        _logger.LogInformation("Start loading history");
        var generatedBmp = (await _bmpRepository.GetAllIds()).ToArray();
        var generated3Mf = (await _tMfRepository.GetAllIds()).ToArray();
        _logger.LogInformation("Cleared");
        var events = await _eventRepository.GetAllEvents();
        foreach (var e in events)
        {
            if(!generatedBmp.Contains(e.WorldId))
                await _bmpRepository.Insert(e.World);
            if(!generated3Mf.Contains(e.WorldId))
                await _synchronizer.Execute(new (e.WorldId));
            dtos.Add(e.ToDtoWithChanges());
            _logger.LogInformation("Loaded {name}", e.Name);
        }
        _logger.LogInformation("History loaded");
        await File.WriteAllTextAsync(_rootFolderProvider.GetPath("events.json"), 
            JsonConvert.SerializeObject(dtos, Formatting.Indented),
            Encoding.UTF8);
        _logger.LogInformation("Events metadata saved");
        await _loadGitRepo.Execute(new ());
        Console.WriteLine("Git repository loaded");
        await _copyDataToWebApp.Execute(new ());
        Console.WriteLine("Data copied");
        await _saveChangesToGitRepo.Execute(new ());
        Console.WriteLine("Changes saved to Git repository");
        _logger.LogInformation("Finished");
    }
}