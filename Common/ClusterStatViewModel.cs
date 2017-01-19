using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class ClusterStatViewModel : BatteryStatViewModel, IDisposable
    {
        protected new double voltageState;

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

                var protecList = SeriesVm.Where(bvm => 
                ((bvm.ChargeState != 0) && (bvm.CurrentForeColor == System.Drawing.Color.Red)) ||
                ((bvm.VoltageState != 0) && (bvm.VoltageForeColor == System.Drawing.Color.Red)) ||
                ((bvm.TemperatureState != 0) && (bvm.TemperatureForeColor == System.Drawing.Color.Red)));

                if (protecList.Count() > 0)
                {
                    var needToCheck = protecList.Select(bvm => bvm.Address).
                       Aggregate((a, b) => string.Format("{0},{1}", a,b));
                    Protection = string.Format("Check batteries :{0}",needToCheck);

                    ProtectionBackColor = System.Drawing.Color.Red;
                }
                else
                {
                    ProtectionBackColor =  SystemColors.Control;
                    Protection = string.Empty;
                }
            }
        }

        public override double Voltage
        {
            get { return this.voltageState; }
            set
            {
                if (value != this.voltageState)
                {
                    this.voltageState = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override int Temperature
        {
            get { return (this.temperature); }
            set
            {
                if (value != this.temperature)
                {
                    this.temperature = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public override double Current
        {
            get { return (this.current); }
            set
            {
                if (value != this.current)
                {
                    this.current = value;
                    NotifyPropertyChanged();
                }
            }
        }        

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
