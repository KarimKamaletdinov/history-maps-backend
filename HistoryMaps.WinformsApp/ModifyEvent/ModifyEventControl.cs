using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HistoryMaps
{
    public partial class ModifyEventControl : UserControl, IModifyEventView
    {
        public WorldDto World { set {  } }
        public event Action<WorldDto> Save;

        public ModifyEventControl()
        {
            InitializeComponent();
            Save += _ => { };
        }
    }
}
