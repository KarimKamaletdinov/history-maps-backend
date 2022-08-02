namespace HistoryMaps;

public partial class AddEventDialog : Form
{
    public event Action<int, int?, string> Save;
    public event Action Cancel;
    public event Action? SetMessage;

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
        Close();
    }

    private void _cancel_Click(object sender, EventArgs e)
    {
        Close();
        Cancel();
    }
}