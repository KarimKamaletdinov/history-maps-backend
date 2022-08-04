using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
namespace HistoryMaps;

public partial class MainForm : Form, IView
{
    public event Action? UpdateEvents;
    public event Action<CreateEventDto>? CreateEvent;
    public event Action<EventBitmapDto>? UpdateEvent;
    public event Action<EventDto>? DeleteEvent;
    public event Action<EventDto>? EventSelected;
    public event Action? ReloadHistory;
    public event Action? LoadAddedHistory;
    private IEnumerable<EventDto>? _events;

    public new IEnumerable<EventDto> Events
    {
        set
        {
            _events = value;
            _eventsListControl.Events = value;
        }
    }

    public EventBitmapDto? CurrentEvent
    {
        set
        {
            if (value != null)
            {
                _eventsListControl.Visible = false;
                var modify = new ModifyEventControl
                {
                    Dock = DockStyle.Fill,
                    World = value.World
                };
                modify.Save += w => UpdateEvent?.Invoke(new(value.Event with {WorldId = Guid.NewGuid()}, w));
                modify.Delete += () => Delete(value.Event);
                modify.Back += () => { 
                    Controls.Remove(modify);
                    UpdateEvents?.Invoke();
                    _eventsListControl.Visible = true;
                };
                Controls.Add(modify);
            }
        }
    }

    private void Delete(EventDto e)
    {
        var dialog = new TextDialog("Удалить событие?");
        var result = dialog.ShowDialog(this);
        if (result == DialogResult.OK)
            DeleteEvent?.Invoke(e);
    }

    public MainForm()
    {
        InitializeComponent();
        _eventsListControl.AddEvent += AddEvent;
        _eventsListControl.ReloadHistory += () => ReloadHistory?.Invoke();
        _eventsListControl.LoadAddedHistory += () => LoadAddedHistory?.Invoke();
        _eventsListControl.EventSelected += e => EventSelected?.Invoke(e);
    }

    private void AddEvent()
    {
        var dialog = new AddEventDialog();
        var result = dialog.ShowDialog(this);
        if (result == DialogResult.OK)
        {
            var e = new CreateEventDto(dialog.EventYear, dialog.EventEndYear, dialog.EventName, Guid.NewGuid());
            CreateEvent?.Invoke(e);
            EventSelected?.Invoke(new(e.Year, GenerateId(e.Year), e.EndYear, e.Name, e.WorldId));
        }
    }

    private int GenerateId(int year)
    {
        return _events!.OrderBy(x => x.Year).ThenBy(x => x.Id).Last(x => x.Year <= year).Id + 1;
    }
}