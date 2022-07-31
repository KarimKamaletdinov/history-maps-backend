namespace HistoryMaps
{
    public partial class ModifyEventControl : UserControl, IModifyEventView
    {
        public WorldBitmapDto World
        {
            set
            {
                _picture.Image = value.Bitmap;
                _countries = value.Countries;
            }
        }

        public event Action<WorldBitmapDto> Save;
        public event Action Back;

        private IEnumerable<CountryColorDto> _countries = Array.Empty<CountryColorDto>();

        public ModifyEventControl()
        {
            InitializeComponent();
            Save += _ => { };
            Back += () => { };
        }

        private void SetPixel(int x, int y)
        {
            ((Bitmap)_picture.Image).SetPixel((int)((float)x / _picture.Width * Map.Width), (int)((float)y / _picture.Height * Map.Height), 
                _countries.ElementAt(0).Color);
            _picture.Invalidate();
        }

        private void _picture_MouseMove(object _, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.None)
                SetPixel(e.X, e.Y);
        }

        private void _picture_MouseDown(object _, MouseEventArgs e) => SetPixel(e.X, e.Y);

        private void _back_Click(object _, EventArgs __) => Back();

        private void _save_Click(object _, EventArgs __) => Save(new((Bitmap)_picture.Image, _countries));
    }
}
