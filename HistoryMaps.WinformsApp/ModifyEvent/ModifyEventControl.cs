namespace HistoryMaps
{
    public partial class ModifyEventControl : UserControl, IModifyEventView
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

        public event Action<WorldBitmapDto> Save;
        public event Action Back;

        private IEnumerable<CountryColorDto> _countries = Array.Empty<CountryColorDto>();
        private CountryColorDto? _selectedCountry;

        public ModifyEventControl()
        {
            InitializeComponent();
            Save += _ => { };
            Back += () => { };
        }

        private void SetPixel(int x, int y)
        {
            var wx = (int)((float)x / _picture.Width * Map.Width);
            var wy = (int)((float)y / _picture.Height * Map.Height);
            if (((Bitmap)_picture.Image).GetPixel(wx, wy) != Map.WaterColor)
            {
                ((Bitmap)_picture.Image).SetPixel(wx, wy,
                    _selectedCountry?.Color ?? Color.White);
            }

            _picture.Invalidate();
        }

        private void _picture_MouseMove(object _, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None)
                SetPixel(e.X, e.Y);
        }

        private void _picture_MouseDown(object _, MouseEventArgs e) => SetPixel(e.X, e.Y);

        private void _back_Click(object _, EventArgs __) => Back();

        private void _save_Click(object _, EventArgs __) => Save(new((Bitmap)_picture.Image, _countries));

        private void _selectCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedCountry = _countries.FirstOrDefault(x => x.Name == _selectCountry.SelectedItem.ToString());
        }
    }
}
