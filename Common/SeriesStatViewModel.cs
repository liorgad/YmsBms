using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class SeriesStatViewModel : BatteryStatViewModel, IDisposable
    {
        protected new double voltageState;
        public IEnumerable<BatteryStatViewModel> SeriesBatteriesAddresses { get; set; }

        //Dictionary<string, PropertyInfo> properyInfoMap;
        System.Threading.Timer updateTimer;
        public SeriesStatViewModel(SynchronizationContext syncCtx, string name, IEnumerable<BatteryStatViewModel> selectedBatteries) : base(syncCtx)
        {
            //properyInfoMap =(this.GetType().GetProperties().ToDictionary(pi => pi.Name));
            this.Address = string.Format("{0}({1})", name, selectedBatteries.Select(bvm => bvm.Address).Aggregate((addr1, addr2) => string.Format("{0},{1}", addr1, addr2)));
            SeriesBatteriesAddresses = selectedBatteries;

            IsSeries = true;
            //SeriesBatteriesAddresses = SharedData.Default.BatteryPackContainer.Values.Where(bp => selectedBatteries.Contains(bp.Address));

            //foreach (var item in clusterBatteriesAddresses)
            //{
            //    item.PropertyChanged += Item_PropertyChanged;
            //}

            Start();
        }

        public override void Start()
        {
            updateTimer = new Timer((s) =>
            {
                UpdateSeriesProperties();

            }, null, TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(2.0));
        }

        public void UpdateSeriesProperties()
        {
            SOC = Convert.ToInt32(SeriesBatteriesAddresses.Select(bvm => bvm.SOC).Average());

            Voltage = SeriesBatteriesAddresses.Select(bvm => bvm.Voltage).Average();

            Current = SeriesBatteriesAddresses.Select(bvm => bvm.Current).Sum();

            Temperature = SeriesBatteriesAddresses.Select(bvm => bvm.Temperature).Max();

            ChargeState = SeriesBatteriesAddresses.Select(bvm => bvm.ChargeState).Aggregate((c1, c2) => (ushort)(c1 | c1));

            TemperatureState = SeriesBatteriesAddresses.Select(bvm => bvm.TemperatureState).Aggregate((c1, c2) => (ushort)(c1 | c1));

            VoltageState = SeriesBatteriesAddresses.Select(bvm => bvm.VoltageState).Aggregate((c1, c2) => (ushort)(c1 | c1));

            var protecList = SeriesBatteriesAddresses.Where(bvm =>
               ((bvm.ChargeState != 0) && (bvm.CurrentForeColor == System.Drawing.Color.Red)) ||
               ((bvm.VoltageState != 0) && (bvm.VoltageForeColor == System.Drawing.Color.Red)) ||
               ((bvm.TemperatureState != 0) && (bvm.TemperatureForeColor == System.Drawing.Color.Red)));

            var currentThreashold = Common.Configuration.Default.CurrentThreashold;

            if (protecList.Count() > 0)
            {
                var needToCheck = protecList.Select(bvm => bvm.Address).
                   Aggregate((a, b) => string.Format("{0},{1}", a, b));
                Protection = string.Format("Check batteries :{0}", needToCheck);

                ProtectionBackColor = System.Drawing.Color.Red;
            }
            else if (Current > 0 && Math.Abs(Current) > currentThreashold)
            {
                ProtectionBackColor = SystemColors.Control;
                Protection = "charge status";
                CurrentForeColor = Color.Green;
            }
            else if (Current < 0 && Math.Abs(Current) > currentThreashold)
            {
                ProtectionBackColor = SystemColors.Control;
                Protection = "discharge status";
                CurrentForeColor = Color.Green;
            }
            else
            {
                ProtectionBackColor = SystemColors.Control;
                Protection = string.Empty;
                CurrentForeColor = Color.Black;
            }

            Debug.WriteLine(Address + "SOC=" + SOC + " Voltage=" + Voltage + " Current=" + Current + " Temp=" + Temperature);
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

        public override Color CurrentForeColor
        {
            get { return (this.currentForeColor); }
            set
            {
                if (value != this.currentForeColor)
                {
                    this.currentForeColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public void Dispose()
        {
            if (null != updateTimer)
            {
                updateTimer.Dispose();
            }
        }
    }
}
