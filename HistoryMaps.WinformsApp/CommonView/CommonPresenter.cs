namespace HistoryMaps;

public class CommonPresenter
{
    private readonly EventsListPresenter _eventsListPresenter;
    private readonly ModifyEventPresenter _modifyEventPresenter;

    public CommonPresenter(EventsListPresenter eventsListPresenter, ModifyEventPresenter modifyEventPresenter)
    {
        _eventsListPresenter = eventsListPresenter;
        _modifyEventPresenter = modifyEventPresenter;
    }

    public void Initialize(ICommonView view)
    {
        _eventsListPresenter.ShowEvent += e => _modifyEventPresenter.Initialize(view.ShowModifyEventView(), e);
        _eventsListPresenter.Initialize(view.ShowEventsListView());
    }
}