using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
namespace HistoryMaps;

public partial class MainForm : Form, IView
{
    public event Action<EventDto>? CreateEvent;
    public event Action<EventBitmapDto>? UpdateEvent;
    public event Action<EventDto>? DeleteEvent;
    public event Action<EventDto>? EventSelected;
    public event Action? ReloadHistory;
    public event Action? LoadAddedHistory;

    public new IEnumerable<EventDto> Events
    {
        set => _eventsListControl.Events = value;
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
                modify.Save += w => UpdateEvent?.Invoke(value with { World = w });
                modify.Delete += () => Delete(value.Event);
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
        _eventsListControl.ReloadHistory += ReloadHistory;
        _eventsListControl.LoadAddedHistory += LoadAddedHistory;
    }

    private void AddEvent()
    {
        var dialog = new AddEventDialog();
        var result = dialog.ShowDialog(this);
        if (result == DialogResult.OK)
        {
            var e = new EventDto(dialog.EventYear, dialog.EventEndYear, dialog.EventName, Guid.NewGuid());
            CreateEvent?.Invoke(e);
            EventSelected?.Invoke(e);
        }
    }
}