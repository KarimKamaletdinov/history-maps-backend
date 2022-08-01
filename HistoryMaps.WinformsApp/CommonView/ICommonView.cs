namespace HistoryMaps;

public interface ICommonView
{
    public IEventsListView ShowEventsListView();
    public IModifyEventView ShowModifyEventView();
    public IAddEventView ShowAddEventView();
}