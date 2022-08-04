using System.Security.Cryptography.X509Certificates;
using Point = System.Drawing.Point;

namespace HistoryMaps;

public partial class ModifyEventControl : UserControl
{
    public WorldBitmapDto World
    {
        set
        {
            _picture.Image = value.Bitmap;
            Countries = value.Countries;
        }
    }

    public IEnumerable<CountryColorDto> Countries
    {
        get => _countries;
        set
        {
            _selectCountry.Items.Clear();
            _selectCountry.Items.Add("-");
            foreach (var country in value)
                _selectCountry.Items.Add(country.Name);
            _countries = value;
        }
    }

    public event Action<WorldBitmapDto>? Save;
    public event Action? Delete;
    public event Action? Back;

    private IEnumerable<CountryColorDto> _countries = Array.Empty<CountryColorDto>();
    private CountryColorDto? _selectedCountry;
    private int _zoom = 1;
    private Point? _lastMouse;
    private Point? _lastAddedPoint;
    private bool _isFill;

    public ModifyEventControl()
    {
        InitializeComponent();
    }

    private void SetPixel(int x, int y)
    {
        var wx = (int)((float)x / _picture.Width * Map.Width + 0.5f);
        var wy = (int)((float)y / _picture.Height * Map.Height + 0.5f);
        if (((Bitmap)_picture.Image).GetPixel(wx, wy) != Map.WaterColor)
        {
            ((Bitmap)_picture.Image).SetPixel(wx, wy, SelectedCountryColor());
        }
        _picture.Invalidate();
    }

    private void DrawLine(int x1, int y1, int x2, int y2)
    {
        if (x2 - x1 > y2 - y1)
        {
            for (var x = x1; x <= x2; x++)
            {
                var y = y1 + (y2 - y1) / (x2 - x + 1);
                SetPixel(x, y);
            }
        }
        else
        {
            for (var y = y1; y <= y2; y++)
            {
                var x = x1 + (x2 - x1) / (y2 - y + 1);
                SetPixel(x, y);
            }
        }
    }

    private void Fill(int startX, int startY)
    {
        var wx = (int)((float)startX / _picture.Width * Map.Width + 0.5f);
        var wy = (int)((float)startY / _picture.Height * Map.Height + 0.5f);
        var oldColor = ((Bitmap)_picture.Image).GetPixel(wx, wy);
        var toFill = new List<Point> { new(wx, wy) };

        void Add(Point p, Color old)
        {
            if(p.X < 0 || p.Y < 0 || p.X >= _picture.Image.Width || p.Y >= _picture.Image.Height) return;
            var color = ((Bitmap)_picture.Image).GetPixel(p.X, p.Y);
            if (color == old && !toFill.Contains(p))
                toFill.Add(p);
        }

        while (toFill.Count > 0)
        {
            var x = toFill[0].X;
            var y = toFill[0].Y;
            toFill.RemoveAt(0);
            ((Bitmap)_picture.Image).SetPixel(x, y, SelectedCountryColor());

            Add(new(x, y + 1), oldColor);
            Add(new(x, y - 1), oldColor);
            Add(new(x + 1, y), oldColor);
            Add(new(x - 1, y), oldColor);
        }
        _picture.Invalidate();
    }

    private Color SelectedCountryColor()
    {
        return _selectedCountry?.Color ?? Color.White;
    }

    private void _picture_MouseMove(object _, MouseEventArgs e)
    {
        switch (e.Button)
        {
            case MouseButtons.Left:
                if(_lastAddedPoint == null)
                    SetPixel(e.X, e.Y);
                else
                    DrawLine(_lastAddedPoint.Value.X, _lastAddedPoint.Value.Y, e.X, e.Y);
                _lastAddedPoint = e.Location;
                break;
            case MouseButtons.Right:
                if (_lastMouse != null)
                {
                    var x = _picture.Location.X - _lastMouse.Value.X + e.X;
                    var y = _picture.Location.Y - _lastMouse.Value.Y + e.Y;
                    if(x + _picture.Width >= Width && y + _picture.Height >= Height - _flowPanel.Height
                                                   && x <= 0 && y <= _flowPanel.Height)
                        _picture.Location = new Point(x, y);
                }

                break;
        }
    }

    private void _picture_MouseDown(object _, MouseEventArgs e)
    {
        _lastMouse = e.Location;
        if (e.Button == MouseButtons.Left)
        {
            _lastAddedPoint = e.Location;
            if(_isFill)
                Fill(e.X, e.Y);
            else
                SetPixel(e.X, e.Y);
        }
    }

    private void _back_Click(object _, EventArgs __) => Back?.Invoke();

    private void _save_Click(object _, EventArgs __)
    {
        Save?.Invoke(new((Bitmap)_picture.Image, _countries));
    }

    private void _selectCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        _selectedCountry = _countries.FirstOrDefault(x => x.Name == _selectCountry.SelectedItem.ToString());
    }

    private void _delete_Click(object sender, EventArgs e)
    {
        var dialog = new TextDialog("Удалить событие?");
        dialog.ShowDialog(this);
        if (dialog.DialogResult == DialogResult.OK)
        {
            Delete?.Invoke();
        }
    }

    private void _plus_Click(object sender, EventArgs e)
    {
        if (_zoom < 3)
        {
            _zoom ++;
            _picture.Size *= 2;
            _picture.Location -= _picture.Size / 4;
        }
    }

    private void _minus_Click(object sender, EventArgs e)
    {
        if (_zoom > 1)
        {
            _zoom --;
            _picture.Size /= 2;
            _picture.Location += _picture.Size / 2;
        }
    }

    private void _picture_MouseUp(object sender, MouseEventArgs e)
    {
        _lastAddedPoint = null;
    }

    private void _pencil_Click(object sender, EventArgs e)
    {
        _pencil.Enabled = false;
        _fill.Enabled = true;
        _isFill = false;
    }

    private void _fill_Click(object sender, EventArgs e)
    {
        _pencil.Enabled = true;
        _fill.Enabled = false;
        _isFill = true;
    }
}