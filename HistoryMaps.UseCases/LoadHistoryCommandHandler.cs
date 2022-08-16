using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HistoryMaps;

public class LoadHistoryCommandHandler : ICommandHandler<LoadHistory>
{
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IVolumeWorldRepository _tMfRepository;
    private readonly ICommandHandler<SynchronizeWorld> _synchronizer;
    private readonly ILogger<LoadHistoryCommandHandler> _logger;
    private readonly IRootFolderProvider _rootFolderProvider;

    public LoadHistoryCommandHandler(IWorldBmpRepository bmpRepository, 
        IEventRepository eventRepository, IVolumeWorldRepository tMfRepository, ICommandHandler<SynchronizeWorld> synchronizer,
        ILogger<LoadHistoryCommandHandler> logger, IRootFolderProvider rootFolderProvider)
    {
        _bmpRepository = bmpRepository;
        _eventRepository = eventRepository;
        _tMfRepository = tMfRepository;
        _synchronizer = synchronizer;
        _logger = logger;
        _rootFolderProvider = rootFolderProvider;
    }

    public void Execute(LoadHistory command)
    {
        var dtos = new List<EventChangesDto>();
        _logger.LogInformation("Start loading history");
        _bmpRepository.ClearAll();
        _tMfRepository.ClearAll();
        _logger.LogInformation("Cleared");
        var events = _eventRepository.GetAllEvents();
        foreach (var e in events)
        {
            _bmpRepository.Insert(e.World);
            if(command.Generate3Mf)
                _synchronizer.Execute(new (e.World.Id));
            _logger.LogInformation("Loaded {Name}", e.Name);
            dtos.Add(e.ToDto());
        }
        File.WriteAllText(_rootFolderProvider.GetPath("events.json"), 
            JsonConvert.SerializeObject(dtos, Formatting.Indented),
            Encoding.UTF8);
        _logger.LogInformation("Finished");
    }
}
