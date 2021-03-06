using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HistoryMaps;

public class LoadHistoryCommandHandler : ICommandHandler<LoadHistory>
{
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IWorld3MfRepository _tMfRepository;
    private readonly ICommandHandler<SynchronizeWorld> _synchronizer;
    private readonly ILogger<LoadHistoryCommandHandler> _logger;
    private readonly IRootFolderProvider _rootFolderProvider;

    public LoadHistoryCommandHandler(IWorldBmpRepository bmpRepository, 
        IEventRepository eventRepository, IWorld3MfRepository tMfRepository, ICommandHandler<SynchronizeWorld> synchronizer,
        ILogger<LoadHistoryCommandHandler> logger, IRootFolderProvider rootFolderProvider)
    {
        _bmpRepository = bmpRepository;
        _eventRepository = eventRepository;
        _tMfRepository = tMfRepository;
        _synchronizer = synchronizer;
        _logger = logger;
        _rootFolderProvider = rootFolderProvider;
    }

    public async Task Execute(LoadHistory command)
    {
        var dtos = new List<EventChangesDto>();
        _logger.LogInformation("Start loading history");
        await _bmpRepository.ClearAll();
        await _tMfRepository.ClearAll();
        _logger.LogInformation("Cleared");
        var events = await _eventRepository.GetAllEvents();
        foreach (var e in events)
        {
            await _bmpRepository.Insert(e.World);
            if(command.Generate3Mf)
                await _synchronizer.Execute(new (e.World.Id));
            _logger.LogInformation("Loaded {Name}", e.Name);
            dtos.Add(e.ToDtoWithChanges());
        }
        await File.WriteAllTextAsync(_rootFolderProvider.GetPath("events.json"), 
            JsonConvert.SerializeObject(dtos, Formatting.Indented),
            Encoding.UTF8);
        _logger.LogInformation("Finished");
    }
}
