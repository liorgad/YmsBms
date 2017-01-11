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
    public partial class ClusterStatistics : UserControl
    {
        public ClusterStatistics(BatteryStatViewModel clusterVM)
        {
            InitializeComponent();

            clusterStatViewModelBindingSource.DataSource = clusterVM;
        }
        
        private void button_delete_Click(object sender, EventArgs e)
        {
            BatteryStatViewModel vm;
            SharedData.Default.BatteryPackContainer.TryRemove("cluster", out vm);
            this.Dispose();
        }
    }
}
