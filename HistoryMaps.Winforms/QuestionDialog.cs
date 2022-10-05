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
    public partial class QuestionDialog : Form
    {
        public string Question
        {
            get => _text.Text;
            set => _text.Text = value;
        }

        public string Answer { get; set; } = string.Empty;

        public QuestionDialog()
        {
            InitializeComponent();
        }

        private void _ok_Click(object sender, EventArgs e)
        {
            Answer = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
