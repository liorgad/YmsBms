using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class SeriesStatViewModel : BatteryStatViewModel, IDisposable
    {
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

            var protecList = SeriesBatteriesAddresses.Where(bvm => (bvm.ChargeState != 0) || (bvm.VoltageState != 0) || (bvm.TemperatureState != 0));

            if(protecList.Count() > 0)
            {
                Protection = protecList.Select(bvm => string.Format("{0}: {1}", bvm.Address, bvm.Protection)).
                   Aggregate((a, b) => string.Format("{0}{1}{2}", a, Environment.NewLine, b));

                ProtectionBackColor = System.Drawing.Color.Orange;
            }
            else
            {
                ProtectionBackColor = System.Drawing.Color.Transparent;
            }

           

            Debug.WriteLine(Address + "SOC=" + SOC + " Voltage=" + Voltage + " Current=" + Current + " Temp=" + Temperature);
        }

        public override double Voltage
        {
            get;
            set;
        }

        public override int Temperature
        {
            get;
            set;
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
