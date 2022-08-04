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

    public async Task Initialize(IView view)
    {
        view.Events = await _getAllEventsHandler.Execute(new());
        view.EventSelected += async e => view.CurrentEvent = new(e, await _getWorldBitmapHandler.Execute(new(e.WorldId)));
        view.CreateEvent += async e => await _createEventHandler.Execute(new(e));
        view.UpdateEvent += async e => await _modifyEventHandler.Execute(new(e));
        view.DeleteEvent += async e => await _deleteEventHandler.Execute(new(e));
        view.ReloadHistory += async () => await _loadHistoryHandler.Execute(new());
        view.LoadAddedHistory += async () => await _loadAddedHistoryHandler.Execute(new());
    }
}