using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HistoryMaps.Winforms
{
    public partial class MainForm : Form, IView
    {
        public event Action<EventDto>? CreateEvent;
        public event Action<EventDto>? UpdateEvent;
        public event Action<EventDto>? DeleteEvent;
        public event Action<EventDto>? EventSelected;
        public event Action? ReloadHistory;
        public event Action? LoadAddedHistory;
        public new IEnumerable<EventDto> Events { set {  } }
        public WorldBitmapDto? CurrentWorld { set {  } }

        public MainForm()
        {
            InitializeComponent();
        }
    }
}
