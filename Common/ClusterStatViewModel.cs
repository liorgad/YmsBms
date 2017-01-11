using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class ClusterStatViewModel : BatteryStatViewModel, IDisposable
    {
        
        private List<SeriesStatViewModel> seriesVm;
        private System.Threading.Timer updateTimer;
        public ClusterStatViewModel(SynchronizationContext syncCtx,List<SeriesStatViewModel> series) : base(syncCtx)
        {
            this.seriesVm = series;

            Start();
        }

         public override void Start()
        {
            updateTimer = new Timer((s) =>
            {
                UpdateProperties();

            }, null, TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(2.0));
        }

        public void UpdateProperties()
        {
            SOC = Convert.ToInt32(seriesVm.Select(ser => ser.SOC).Min());

            Voltage = seriesVm.Select(bvm => bvm.Voltage).Sum();

            Current = seriesVm.Select(bvm => bvm.Current).Average();

            Temperature = seriesVm.Select(bvm => bvm.Temperature).Max();

            var diff = Math.Abs(seriesVm[0].Voltage - seriesVm[1].Voltage);

            if (diff > Common.Configuration.Default.VoltageDifferenceThreshold)
            {
                Protection = "CLUSTER MIS-BALANCE";
                ProtectionBackColor = System.Drawing.Color.Red;
            }
            else
            {
                Protection = string.Empty;
                ProtectionBackColor = System.Drawing.Color.Transparent;
            }
        }

        public override double Voltage { get; set; }
        public override int Temperature { get; set; }

        public void Dispose()
        {
            if(null != seriesVm && seriesVm.Count > 0)
            {
                foreach (var item in seriesVm)
                {
                    item.Dispose();
                }
            }

            if(null != updateTimer)
            {
                updateTimer.Dispose();
            }
        }
    }
}
