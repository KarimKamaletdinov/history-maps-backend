namespace HistoryMaps
{
    public partial class EventsListControl : UserControl
    {
        public new EventDto[] Events
        {
            set
            {
                _table.Controls.Clear();
                foreach (var ev in value)
                {
                    AddLabel(ev.Year.ToYearString() + 
                             (ev.EndYear != null ? " - " + ev.EndYear?.ToYearString() : ""), 
                        SystemColors.ControlText, () => ShowEvent(ev));
                    AddLabel(ev.Name, SystemColors.ControlText, () => ShowEvent(ev));
                    AddLabel(ev.WorldId.ToString(), Color.Blue, () => ShowEvent(ev));
                }
            }
        }

        public event Action<EventDto> ShowEvent;
        public event Action AddEvent;
        public new event Action Load;
        public event Action LoadAdded;
        public event Action<string> SetMessage;

        public EventsListControl()
        {
            InitializeComponent();
            ShowEvent += _ => { };
            AddEvent += () => { };
            Load += () => { };
            LoadAdded += () => { };
            SetMessage += _ => { };
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

        private void _load_Click(object sender, EventArgs e)
        {
            SetMessage("Перезагрузка истории...");
            Load();
            SetMessage("Загружено");
        }

        private void _loadAdded_Click(object sender, EventArgs e)
        {
            SetMessage("Загрузка добавленной истории...");
            LoadAdded();
            SetMessage("Загружено");
        }
    }
}
