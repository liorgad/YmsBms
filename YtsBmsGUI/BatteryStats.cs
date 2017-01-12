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
    public partial class BatteryStats : UserControl
    {
        private BatteryStatViewModel viewModel;
        private IEventAggregator evAgg { get; set; }

        public string Address { get; private set; }
        public BatteryStats(string address)
        {
            InitializeComponent();
            evAgg = Common.EventAggregatorProvider.EventAggregator;
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
            //BatteryStatViewModel vm;
            //if(SharedData.Default.BatteryPackContainer.TryRemove(Address.ToString(),out vm))
            //{
                evAgg.PublishOnUIThread(new BatteryRemoveView() { Address = this.Address });
            //}
            this.Dispose();            
        }

        public override int GetHashCode()
        {
            if( null != viewModel)
            {
                return viewModel.GetHashCode();
            }

            return base.GetHashCode();
        }
    }
}
