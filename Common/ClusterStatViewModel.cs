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

            IsSeries = true;

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

                var protecList = SeriesVm.Where(bvm => (bvm.ChargeState != 0) || (bvm.VoltageState != 0) || (bvm.TemperatureState != 0));

                if (protecList.Count() > 0)
                {
                    Protection = protecList.Select(bvm => string.Format("{0}: {1}", bvm.Address, bvm.Protection)).
                       Aggregate((a, b) => string.Format("{0}{1}{2}", a, Environment.NewLine, b));

                    ProtectionBackColor = System.Drawing.Color.Orange;
                }
                else
                {
                     ProtectionBackColor =  System.Drawing.Color.Transparent;
                }

                //Protection = SeriesVm.Where(bvm => bvm.Protection != string.Empty).Select(bvm => string.Format("{0}: {1}", bvm.Address, bvm.Protection)).
                //    Aggregate((a, b) => string.Format("{0}{1}{2}",a,Environment.NewLine,b));
                
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
