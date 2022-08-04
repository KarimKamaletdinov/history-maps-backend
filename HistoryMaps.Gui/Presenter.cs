namespace HistoryMaps;

public class Presenter
{
    private readonly IQueryHandler<GetAllEvents, IEnumerable<EventDto>> _getAllEventsHandler;
    private readonly IQueryHandler<GetWorldBitmap, WorldBitmapDto> _getWorldBitmapHandler;
    private readonly ICommandHandler<CreateEvent> _createEventHandler;
    private readonly ICommandHandler<ModifyEvent> _modifyEventHandler;
    private readonly ICommandHandler<DeleteEvent> _deleteEventHandler;
    private readonly ICommandHandler<LoadHistory> _loadHistoryHandler;
    private readonly ICommandHandler<LoadAddedHistory> _loadAddedHistoryHandler;
    private IView? _view;

    public Presenter(IQueryHandler<GetAllEvents, IEnumerable<EventDto>> getAllEventsHandler, 
        ICommandHandler<ModifyEvent> modifyEventHandler, IQueryHandler<GetWorldBitmap, WorldBitmapDto> getWorldBitmapHandler,
        ICommandHandler<CreateEvent> createEventHandler, ICommandHandler<DeleteEvent> deleteEventHandler,
        ICommandHandler<LoadHistory> loadHistoryHandler, ICommandHandler<LoadAddedHistory> loadAddedHistoryHandler)
    {
        _getAllEventsHandler = getAllEventsHandler;
        _modifyEventHandler = modifyEventHandler;
        _getWorldBitmapHandler = getWorldBitmapHandler;
        _createEventHandler = createEventHandler;
        _deleteEventHandler = deleteEventHandler;
        _loadHistoryHandler = loadHistoryHandler;
        _loadAddedHistoryHandler = loadAddedHistoryHandler;
    }

    public void Initialize(IView view)
    {
        view.Events = _getAllEventsHandler.Execute(new());
        view.UpdateEvents += () => view.Events = _getAllEventsHandler.Execute(new());
        view.EventSelected += e => view.CurrentEvent = new(e, _getWorldBitmapHandler.Execute(new(e.WorldId)));
        view.CreateEvent += e => _createEventHandler.Execute(new(e));
        view.UpdateEvent += e => _modifyEventHandler.Execute(new(e));
        view.DeleteEvent += e => _deleteEventHandler.Execute(new(e));
        view.ReloadHistory += () => _loadHistoryHandler.Execute(new());
        view.LoadAddedHistory += () => _loadAddedHistoryHandler.Execute(new());
    }
}