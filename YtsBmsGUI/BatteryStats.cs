using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace YtsBmsGUI
{
    public partial class BatteryStats : UserControl
    {
        private BatteryStatViewModel viewModel;

        public string Address { get; private set; }
        public BatteryStats(string address)
        {
            InitializeComponent();
            Address = address;
            groupBox1.Text = string.Format("Address : {0}", address);
        }

        public BatteryStats(string address, BatteryStatViewModel viewModel) : this(address)
        {
            this.viewModel = viewModel;
            bindingSource_batterStats.DataSource = viewModel;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            BatteryStatViewModel vm;
            SharedData.Default.BatteryPackContainer.TryRemove(Address.ToString(),out vm);
            this.Dispose();            
        }
    }
}
