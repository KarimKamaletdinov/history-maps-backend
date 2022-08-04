namespace HistoryMaps;

public partial class TextDialog : Form
{
    private readonly string _message;

    public TextDialog(string message = "Message")
    {
        _message = message;
        InitializeComponent();
    }

    private void TextDialog_Load(object sender, EventArgs e)
    {
        Text = _message;
        _text.Text = _message;
    }

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