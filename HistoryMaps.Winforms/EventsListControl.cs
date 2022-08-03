namespace HistoryMaps
{
    public partial class EventsListControl : UserControl, IEventsListView
    {
        public new EventDto[] Events
        {
            set
            {
                _table.Controls.Clear();
                foreach (var ev in value)
                {
                    AddLabel(ev.Year.ToYearString() +
                        (ev.EndYear != null ? " - " + ev.EndYear?.ToYearString() : ""));
                    AddLabel(ev.Name);
                    AddLabel(ev.WorldId.ToString(), Color.Blue, () => ShowEvent(ev));
                }
            }
        }

        public event Action<EventDto> ShowEvent;
        public event Action AddEvent;

        public EventsListControl()
        {
            InitializeComponent();
            ShowEvent += _ => { };
            AddEvent += () => { };
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
            AddEvent();
        }
    }
}
