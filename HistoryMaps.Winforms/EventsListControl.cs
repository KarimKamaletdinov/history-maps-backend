namespace HistoryMaps;

public partial class EventsListControl : UserControl
{
    public new IEnumerable<EventDto> Events
    {
        set
        {
            _table.Controls.Clear();
            foreach (var ev in value)
            {
                AddLabel(ev.Year.ToYearString() + 
                         (ev.EndYear != null ? " - " + ev.EndYear?.ToYearString() : ""), 
                    SystemColors.ControlText, () =>
                    {
                        EventSelected!.Invoke(ev);
                    });
                AddLabel(ev.Name, SystemColors.ControlText, () => EventSelected?.Invoke(ev));
                AddLabel(ev.WorldId.ToString(), Color.Blue, () => EventSelected?.Invoke(ev));
            }
        }
    }

    public event Action<EventDto>? EventSelected;
    public event Action? AddEvent;
    public event Action? ReloadHistory;
    public event Action? LoadAddedHistory;

    public EventsListControl()
    {
        InitializeComponent();
        AddLabel("Загрузка");
    }

    private void AddLabel(string text, Color? foreColor = null, Action? onClick = null)
    {
        var label = new Label
        {
            Text = text,
            Dock = DockStyle.Fill,
            ForeColor = foreColor ?? SystemColors.ControlText,
            BackColor = Color.White
        };

        _table.Controls.Add(label);

        if (onClick != null)
            label.Click += (_, _) => onClick();

    }

    private void _add_Click(object sender, EventArgs e)
    {
        AddEvent?.Invoke();
    }

    private void _load_Click(object sender, EventArgs e)
    {
        ReloadHistory?.Invoke();
    }

    private void _loadAdded_Click(object sender, EventArgs e)
    {
        LoadAddedHistory?.Invoke();
    }
}