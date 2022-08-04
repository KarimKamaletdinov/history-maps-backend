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

    public async Task Execute(ModifyEvent c)
    {
        var previous = await _eventRepository.GetPrevious(c.EventBitmap.Event.Year);
        var pw = previous == null 
            ? await _worldBmpRepository.GetBaseWorld() 
            : await _worldBmpRepository.Get(previous.WorldId);
        var worldId = Guid.NewGuid();
        await _worldBmpRepository.InsertBitmap(worldId, c.EventBitmap.World);
        var nw = await _worldBmpRepository.Get(c.EventBitmap.Event.WorldId);
        var changes = Event.ParseChanges(pw, nw);
        var e = new Event(c.EventBitmap.Event.Year, c.EventBitmap.Event.EndYear, c.EventBitmap.Event.Name, changes.ToArray(), pw, nw.Id);
        await _eventRepository.Delete(c.EventBitmap.Event.Year);
        await _eventRepository.Insert(e);
    }
}