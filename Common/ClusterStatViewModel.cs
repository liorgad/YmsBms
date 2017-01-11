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

        public List<SeriesStatViewModel> SeriesVm
        {
            get; set;
        }
        private System.Threading.Timer updateTimer;
        public ClusterStatViewModel(SynchronizationContext syncCtx, List<SeriesStatViewModel> series) : base(syncCtx)
        {
            this.SeriesVm = series;

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
            SOC = Convert.ToInt32(SeriesVm.Select(ser => ser.SOC).Min());

            Voltage = SeriesVm.Select(bvm => bvm.Voltage).Sum();

            Current = SeriesVm.Select(bvm => bvm.Current).Average();

            Temperature = SeriesVm.Select(bvm => bvm.Temperature).Max();

            var diff = Math.Abs(SeriesVm[0].Voltage - SeriesVm[1].Voltage);

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
            if (null != SeriesVm && SeriesVm.Count > 0)
            {
                foreach (var item in SeriesVm)
                {
                    item.Dispose();
                }
            }

            if (null != updateTimer)
            {
                updateTimer.Dispose();
            }
        }
    }
}
