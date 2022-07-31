namespace HistoryMaps
{
    public partial class ModifyEventControl : UserControl, IModifyEventView
    {
        public WorldDto World
        {
            set => _picture.Image = Image.FromFile($"data\\worlds\\{value.Id}.bmp");
        }

        public event Action<Guid> Save;
        public event Action Back;

        public ModifyEventControl()
        {
            InitializeComponent();
            Save += _ => { };
            Back += () => { };
        }

        private void _picture_MouseMove(object _, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.None)
                SetPixel(e.X, e.Y);
        }

        private void _picture_MouseDown(object _, MouseEventArgs e) => SetPixel(e.X, e.Y);

        private void SetPixel(int x, int y)
        {
            ((Bitmap)_picture.Image).SetPixel((int)((float)x / _picture.Width * Map.Width), (int)((float)y / _picture.Height * Map.Height), Color.Black);
            _picture.Invalidate();
        }

        private void _back_Click(object _, EventArgs __)
        {
            Back();
        }
    }
}
