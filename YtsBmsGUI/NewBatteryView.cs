using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YtsBmsGUI
{
    public partial class NewBatteryView : Form
    {
        public byte Address { get; set; }

        public bool IsPartOfCluster { get; set; }
        public NewBatteryView()
        {
            InitializeComponent();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            byte addr;
            if(byte.TryParse(textBox_address.Text,out addr))
            {
                Address = addr;
                IsPartOfCluster = checkBox1.Checked;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Error, please insert address in correct form (0-16)");
                this.DialogResult = DialogResult.Cancel;
            }

            this.Close();            
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
