using Microsoft.Extensions.Logging;

namespace HistoryMaps;

public class LoadAddedHistoryCommandHandler : ICommandHandler<LoadAddedHistory>
{
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IWorld3MfRepository _tMfRepository;
    private readonly ICommandHandler<SynchronizeWorld> _synchronizer;
    private readonly ILogger<LoadAddedHistoryCommandHandler> _logger;

    public LoadAddedHistoryCommandHandler(IWorldBmpRepository bmpRepository, 
        IEventRepository eventRepository, IWorld3MfRepository tMfRepository, ICommandHandler<SynchronizeWorld> synchronizer,
        ILogger<LoadAddedHistoryCommandHandler> logger)
    {
        _bmpRepository = bmpRepository;
        _eventRepository = eventRepository;
        _tMfRepository = tMfRepository;
        _synchronizer = synchronizer;
        _logger = logger;
    }

    public void Execute(LoadAddedHistory command)
    {
        _logger.LogInformation("Start loading history");
        var generatedBmp = _bmpRepository.GetAllIds().ToArray();
        var generated3Mf = _tMfRepository.GetAllIds().ToArray();
        _logger.LogInformation("Cleared");
        var events = _eventRepository.GetAllEvents();
        foreach (var e in events)
        {
            if(!generatedBmp.Contains(e.WorldId))
                _bmpRepository.Insert(e.World);
            if(!generated3Mf.Contains(e.WorldId) && command.Generate3Mf)
                _synchronizer.Execute(new (e.WorldId));
            _logger.LogInformation("Loaded {name}", e.Name);
        }
        _logger.LogInformation("Finished");
    }
}