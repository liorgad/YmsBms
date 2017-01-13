using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class BatteryStatViewModel : INotifyPropertyChanged
    {
        private double current;
        private double voltage;
        private int temperature;
        private string address;
        private byte version;
        private int soc;
        private bool dfet;
        private bool cfet;
        private string protection;
        private ushort chargeState;
        private ushort temperatureState;
        private ushort voltageState;
        private Color protectionBackColor = Color.Transparent;


        public SynchronizationContext SyncCtx { get; set; }

        public virtual Color DFetColor
        {
            get
            {
                return DFET ? Color.Green : Color.Red;
            }
        }

        public virtual Color CFetColor
        {
            get { return CFET ? Color.Green : Color.Red; }
        }

        public virtual Color ProtectionBackColor
        {
            get
            {
                return this.protectionBackColor;
            }
            set
            {
                if (value != this.protectionBackColor)
                {
                    this.protectionBackColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual ushort ChargeState
        {
            get { return this.chargeState; }
            set
            {
                if (value != this.chargeState)
                {
                    this.chargeState = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual ushort TemperatureState
        {
            get { return this.temperatureState; }
            set
            {
                if (value != this.temperatureState)
                {
                    this.temperatureState = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual ushort VoltageState
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


        public bool IsPartOfCluster { get; set; }
        public virtual double Current
        {
            get { return (this.current) / 100; }
            set
            {
                if (value != this.current)
                {
                    this.current = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual double Voltage
        {
            get { return (this.voltage * 2) / 1000; }
            set
            {
                if (value != this.voltage)
                {
                    this.voltage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual int Temperature
        {
            get { return (this.temperature - 40); }
            set
            {
                if (value != this.temperature)
                {
                    this.temperature = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual string Address
        {
            get { return this.address; }
            set
            {
                if (value != this.address)
                {
                    this.address = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual byte Version
        {
            get { return this.version; }
            set
            {
                if (value != this.version)
                {
                    this.version = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual int SOC
        {
            get { return this.soc; }
            set
            {
                if (value != this.soc)
                {
                    this.soc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public virtual bool DFET
        {
            get { return this.dfet; }
            set
            {
                if (value != this.dfet)
                {
                    this.dfet = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DFetColor");
                }
            }
        }

        public virtual bool CFET
        {
            get { return this.cfet; }
            set
            {
                if (value != this.cfet)
                {
                    this.cfet = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("CFetColor");
                }
            }
        }

        public virtual string Protection
        {
            get
            {
                return this.protection;
            }
            set
            {
                if (value != this.protection)
                {
                    this.protection = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ProtectionBackColor");
                }
            }
        }

        public virtual bool IsSeries { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                SyncCtx.Post((state) =>
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }, null);
            }
        }

        public BatteryStatViewModel(SynchronizationContext syncCtx)
        {
            this.SyncCtx = syncCtx;
            IsSeries = false;
        }

        public override string ToString()
        {
            return string.Format("Address={0} Voltage={1} Current={2} Temp={3} SOC={4} DFET={5} CFET={6} Protection={7} ChargeState={8} TempState={9} VoltState={10}",
                Address, Voltage, Current, Temperature, SOC, DFET, CFET, Protection, ChargeState, TemperatureState, VoltageState);
        }

        public virtual void Start() { }


    }
}
