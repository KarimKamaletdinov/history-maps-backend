using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HistoryMaps;

public class CreateWebAppCommandHandler : ICommandHandler<CreateWebApp>
{
    private readonly ILogger<CreateWebAppCommandHandler> _logger;
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IVolumeWorldRepository _tMfRepository;
    private readonly ICommandHandler<SynchronizeWorld> _synchronizer;
    private readonly ICommandHandler<SynchronizeBaseWorld> _bwSynchronizer;
    private readonly IRootFolderProvider _rootFolderProvider;
    private readonly ICommandHandler<SaveChangesToGitRepo> _saveChangesToGitRepo;

    public CreateWebAppCommandHandler(ILogger<CreateWebAppCommandHandler> logger, IWorldBmpRepository bmpRepository,
        IEventRepository eventRepository, IVolumeWorldRepository tMfRepository,
        ICommandHandler<SynchronizeWorld> synchronizer, IRootFolderProvider rootFolderProvider,
        ICommandHandler<SaveChangesToGitRepo> saveChangesToGitRepo, ICommandHandler<SynchronizeBaseWorld> bwSynchronizer)
    {
        _logger = logger;
        _bmpRepository = bmpRepository;
        _eventRepository = eventRepository;
        _tMfRepository = tMfRepository;
        _synchronizer = synchronizer;
        _rootFolderProvider = rootFolderProvider;
        _saveChangesToGitRepo = saveChangesToGitRepo;
        _bwSynchronizer = bwSynchronizer;
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
        
        if(!File.Exists(_rootFolderProvider.GetPath("worlds", "baseworld.bmp")))
            File.Copy(_rootFolderProvider.GetPath("constants", "base_world.bmp"), 
                _rootFolderProvider.GetPath("worlds", "baseworld.bmp"));
        
        if(!File.Exists(_rootFolderProvider.GetPath("worlds", "baseworld.json")))
            File.Copy(_rootFolderProvider.GetPath("constants", "base_world.json"), 
                _rootFolderProvider.GetPath("worlds", "baseworld.json"));

        if(!_tMfRepository.BaseWorldExists())
            _bwSynchronizer.Execute(new());
        
        foreach (var e in events)
        {
            if(!generatedBmp.Contains(e.WorldId))
                _bmpRepository.Insert(e.World);
            if(!generated3Mf.Contains(e.WorldId))
                _synchronizer.Execute(new (e.WorldId));
            dtos.Add(e.ToDto());
            _logger.LogInformation("Loaded {Name}", e.Name);
        }
        _logger.LogInformation("History loaded");
        File.WriteAllText(_rootFolderProvider.GetPath("worlds", "events.json"), 
            JsonConvert.SerializeObject(dtos, new JsonSerializerSettings{Formatting = Formatting.Indented,ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }}),
            Encoding.UTF8);
        _logger.LogInformation("Events metadata saved");
        _saveChangesToGitRepo.Execute(new ());
        Console.WriteLine("Changes saved to Git repository");
        _logger.LogInformation("Finished");
    }
}