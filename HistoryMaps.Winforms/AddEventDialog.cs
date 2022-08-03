namespace HistoryMaps;

public partial class AddEventDialog : Form, IAddEventView
{
    public event Action<int, int?, string> Save;
    public event Action Cancel;

    public AddEventDialog()
    {
        InitializeComponent();
        Save += (_, _, _) => { };
        Cancel += () => { };
    }

    private void _hasEndYear_CheckedChanged(object sender, EventArgs e) => _endYear.Enabled = _hasEndYear.Checked;

    private void _ok_Click(object sender, EventArgs e)
    {
        Save((int)_year.Value, _hasEndYear.Checked ? (int)_endYear.Value : null, _name.Text);
    }

    private void _cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void AddEventDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
        Cancel();
    }
}