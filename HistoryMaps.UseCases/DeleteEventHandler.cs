namespace HistoryMaps;

public class DeleteEventHandler : ICommandHandler<DeleteEvent>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public Task Execute(DeleteEvent command)
    {
        return _eventRepository.Delete(command.Event.Year);
    }
}