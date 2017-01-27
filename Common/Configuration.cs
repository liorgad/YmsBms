using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;
using NLog;

namespace Common
{
    [DataContract]
    public class Configuration
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger(); 

        [DataMember]
        public string PortName { get; set; }

        [DataMember]
        public int BaudRate { get; set; }

        [DataMember]
        public Parity ParityType { get; set; }

        [DataMember]
        public int DataBits { get; set; }

        [DataMember]
        public StopBits StopBitsType { get; set; }

        [DataMember]

        public Handshake HandShakeType { get; set; }

        [DataMember]
        public int ReadTimeout { get; set; }

        [DataMember]
        public int WriteTimeout { get; set; }

        [DataMember]
        public double SamplingTimerIntervalMilisec { get; set; }

        [DataMember]
        public double VoltageDifferenceThreshold { get; set; }

        [DataMember]
        public int WaitTimePeriodBetweenCommandSendMilliSec { get; set; }

        [DataMember]
        public double CurrentThreashold { get; set; }
        private static Configuration config;

        public static Configuration Default { get { return config; } }

       

        static Configuration()
        {
            config = new Configuration();
        }

        public void Load()
        {
            if (File.Exists("configuration.json"))
            {
                using (var stream = File.OpenRead("configuration.json"))
                {
                    //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Configuration));
                    //config = (Configuration)ser.ReadObject(stream);
                    var buffer = new byte[1024];
                    stream.Read(buffer,0,buffer.Length);
                    config = JsonConvert.DeserializeObject<Configuration>(Encoding.Default.GetString(buffer));
                }
            }
        }


        public void Save()
        {
            try
            {
                using (var stream = File.OpenWrite("configuration.json"))
                {
                    //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Configuration));
                    //ser.WriteObject(stream, config);

                    var output = JsonConvert.SerializeObject(config);
                    var bytes = Encoding.Default.GetBytes(output);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch(Exception e)
            {
                logger.Error(e, "Save Error");
            }
        }

    }
}
