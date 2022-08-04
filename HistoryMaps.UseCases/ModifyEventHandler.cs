namespace HistoryMaps;

public class ModifyEventHandler : ICommandHandler<ModifyEvent>
{
    private readonly IEventRepository _eventRepository;
    private readonly IWorldBmpRepository _worldBmpRepository;

    public ModifyEventHandler(IEventRepository eventRepository, IWorldBmpRepository worldBmpRepository)
    {
        _eventRepository = eventRepository;
        _worldBmpRepository = worldBmpRepository;
    }

    public void Execute(ModifyEvent c)
    {
        var previous = _eventRepository.GetPrevious(c.EventBitmap.Event.Year, c.EventBitmap.Event.Id);
        var pw = previous == null 
            ? _worldBmpRepository.GetBaseWorld() 
            : _worldBmpRepository.Get(previous.WorldId);
        _worldBmpRepository.InsertBitmap(c.EventBitmap.Event.WorldId, c.EventBitmap.World);
        var nw = _worldBmpRepository.Get(c.EventBitmap.Event.WorldId);
        var changes = Event.ParseChanges(pw, nw);
        var e = new Event(c.EventBitmap.Event.Year, c.EventBitmap.Event.EndYear, c.EventBitmap.Event.Name, changes.ToArray(), pw, nw.Id);
        _eventRepository.Delete(c.EventBitmap.Event.Year, c.EventBitmap.Event.Id);
        _eventRepository.Insert(e);
    }
}