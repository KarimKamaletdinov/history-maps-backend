namespace HistoryMaps;

public class GenerateWorldsCommandHandler : ICommandHandler<GenerateWorlds>
{
    private readonly IWorldBmpRepository _bmpRepository;
    private readonly IEventRepository _eventRepository;
    private readonly ICommandHandler<SynchronizeWorld> _synchronizer;

    public GenerateWorldsCommandHandler(IWorldBmpRepository bmpRepository, 
        IEventRepository eventRepository, ICommandHandler<SynchronizeWorld> synchronizer)
    {
        _bmpRepository = bmpRepository;
        _eventRepository = eventRepository;
        _synchronizer = synchronizer;
    }

    public void Execute(GenerateWorlds command)
    {
        _bmpRepository.ClearAll();
        var events = _eventRepository.GetAllEvents();
        foreach (var e in events)
        {
            _bmpRepository.Insert(e.World);
            _synchronizer.Execute(new SynchronizeWorld(e.World.Id));
        }
    }
}