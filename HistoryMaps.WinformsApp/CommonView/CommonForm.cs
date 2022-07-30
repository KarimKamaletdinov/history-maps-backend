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
    public partial class CommonForm : Form, ICommonView
    {
        public CommonForm()
        {
            InitializeComponent();
        }

        public IEventsListView ShowEventsListView()
        {
            Controls.Clear();
            var view = new EventsListControl();
            Controls.Add(view);
            return view;
        }

        public IModifyEventView ShowModifyEventView()
        {
            Controls.Clear();
            var view = new ModifyEventControl();
            Controls.Add(view);
            return view;
        }
    }
}
