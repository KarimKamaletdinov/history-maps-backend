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
    public partial class EventsListControl : UserControl, IEventsListView
    {
        public new EventDto[] Events
        {
            set
            {
                foreach (var ev in value)
                {
                    _table.Controls.Add(new Label
                    {
                        Text = ev.Year.ToYearString() +
                               (ev.EndYear != null ? " " + ev.EndYear?.ToYearString() : "")
                    });
                    _table.Controls.Add(new Label
                    {
                        Text = ev.Name
                    });
                    var idLabel = new Label
                    {
                        Text = ev.WorldId.ToString(),
                        ForeColor = Color.Blue
                    };
                    idLabel.Click += (_, _) => ShowEvent(ev);
                    _table.Controls.Add(idLabel);
                }
            }
        }

        public event Action<EventDto> ShowEvent;

        public EventsListControl()
        {
            InitializeComponent();
            ShowEvent += _ => { };
        }
    }
}
