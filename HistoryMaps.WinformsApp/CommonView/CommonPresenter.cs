namespace HistoryMaps;

public class CommonPresenter
{
    private readonly EventsListPresenter _eventsListPresenter;
    private readonly ModifyEventPresenter _modifyEventPresenter;
    private readonly AddEventPresenter _addEventPresenter;

    public CommonPresenter(EventsListPresenter eventsListPresenter, ModifyEventPresenter modifyEventPresenter, 
        AddEventPresenter addEventPresenter)
    {
        _eventsListPresenter = eventsListPresenter;
        _modifyEventPresenter = modifyEventPresenter;
        _addEventPresenter = addEventPresenter;
    }

    public void Initialize(ICommonView view)
    {
        _eventsListPresenter.ShowEvent += e => _modifyEventPresenter.Initialize(view.ShowModifyEventView(), e);
        _eventsListPresenter.AddEvent += () => _addEventPresenter.Initialize(view.ShowAddEventView());
        _modifyEventPresenter.ShowEventsListView += () => _eventsListPresenter.Initialize(view.ShowEventsListView());
        _addEventPresenter.ShowEventsListView += () => 
            _eventsListPresenter.Initialize(view.ShowEventsListView());
        _addEventPresenter.ShowModifyEventView += e => _modifyEventPresenter.Initialize(view.ShowModifyEventView(), e);
        _eventsListPresenter.Initialize(view.ShowEventsListView());
    }
}