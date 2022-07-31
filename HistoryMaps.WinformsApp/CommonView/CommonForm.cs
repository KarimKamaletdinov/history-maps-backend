namespace HistoryMaps
{
    public partial class CommonForm : Form, ICommonView
    {
        public CommonForm()
        {
            InitializeComponent();
        }

        public IEventsListView ShowEventsListView()
        {
            Controls.Clear();
            var view = new EventsListControl { Dock = DockStyle.Fill };
            Controls.Add(view);
            return view;
        }

        public IModifyEventView ShowModifyEventView()
        {
            Controls.Clear();
            var view = new ModifyEventControl { Dock = DockStyle.Fill };
            Controls.Add(view);
            return view;
        }
    }
}
