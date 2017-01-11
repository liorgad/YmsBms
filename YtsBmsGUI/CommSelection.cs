using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YtsBmsGUI
{
    public partial class CommSelection : Form
    {
        public string SelectedComm { get; private set; }
        public CommSelection()
        {
            InitializeComponent();
            SelectedComm = string.Empty;
        }

        private void CommSelection_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            SelectedComm = (string)comboBox1.SelectedItem;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
