using Microsoft.Extensions.Logging;

namespace HistoryMaps;

public class LoadHistoryCommandHandler : ICommandHandler<LoadHistory>
{
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IWorld3MfRepository _tMfRepository;
    private readonly ICommandHandler<SynchronizeWorld> _synchronizer;
    private readonly ILogger<LoadHistoryCommandHandler> _logger;

    public LoadHistoryCommandHandler(IWorldBmpRepository bmpRepository, 
        IEventRepository eventRepository, IWorld3MfRepository tMfRepository, ICommandHandler<SynchronizeWorld> synchronizer,
        ILogger<LoadHistoryCommandHandler> logger)
    {
        _bmpRepository = bmpRepository;
        _eventRepository = eventRepository;
        _tMfRepository = tMfRepository;
        _synchronizer = synchronizer;
        _logger = logger;
    }

    public void Execute(LoadHistory command)
    {
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
        }
        _logger.LogInformation("Finished");
    }
}
