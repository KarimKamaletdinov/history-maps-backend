namespace HistoryMaps;

public class CreateEventHandler : ICommandHandler<CreateEvent>
{
    private readonly IWorldBmpRepository _worldBmpRepository;
    private readonly IEventRepository _eventRepository;

    public CreateEventHandler(IWorldBmpRepository worldBmpRepository, IEventRepository eventRepository)
    {
        _worldBmpRepository = worldBmpRepository;
        _eventRepository = eventRepository;
    }

    public async Task Execute(CreateEvent command)
    {
        var id = await _eventRepository.GenerateId(command.Event.Year);
        var previous = await _eventRepository.GetPrevious(command.Event.Year, id);
        var pw = previous == null
            ? await _worldBmpRepository.GetBaseWorld()
            : await _worldBmpRepository.Get(previous.WorldId);
        var e = new Event(command.Event.Year, command.Event.EndYear, command.Event.Name, Array.Empty<IChange>(), pw, command.Event.WorldId);
        await _eventRepository.Insert(e);
        pw.Id = command.Event.WorldId;
        await _worldBmpRepository.Insert(pw);
    }
}