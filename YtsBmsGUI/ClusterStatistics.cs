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
using Caliburn.Micro;

namespace YtsBmsGUI
{
    public partial class ClusterStatistics : UserControl
    {
        BatteryStatViewModel clusterVm;
        IEventAggregator evAgg { get; set; }
        public ClusterStatistics(BatteryStatViewModel clusterVM)
        {
            InitializeComponent();

            evAgg = EventAggregatorProvider.EventAggregator;

            clusterStatViewModelBindingSource.DataSource = clusterVM;
            this.clusterVm = clusterVM;
        }
        
        private void button_delete_Click(object sender, EventArgs e)
        {

            //BatteryStatViewModel vm;
            //if (SharedData.Default.BatteryPackContainer.TryRemove("cluster", out vm))
            //{
            if (null != clusterVm)
            {
                evAgg.PublishOnUIThread(new BatteryRemoveView() { Address = clusterVm.Address });
            }
            //}
            this.Dispose();

            //BatteryStatViewModel vm;
            //SharedData.Default.BatteryPackContainer.TryRemove(("cluster", out vm);
            //this.Dispose();
        }
    }
}
