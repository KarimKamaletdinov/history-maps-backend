namespace HistoryMaps;

public partial class AddEventDialog : Form
{
    public int EventYear => (int)_year.Value;
    public int? EventEndYear => _hasEndYear.Checked ? (int)_endYear.Value : null;
    public string EventName => _name.Text;

    public AddEventDialog()
    {
        InitializeComponent();
    }

    private void _hasEndYear_CheckedChanged(object sender, EventArgs e) => _endYear.Enabled = _hasEndYear.Checked;

    private void _ok_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void _cancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}