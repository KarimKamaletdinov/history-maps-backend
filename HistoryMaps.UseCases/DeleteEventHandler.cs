namespace HistoryMaps;

public class DeleteEventHandler : ICommandHandler<DeleteEvent>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public void Execute(DeleteEvent command)
    {
        _eventRepository.Delete(command.Event.Year, command.Event.Id);
    }
}