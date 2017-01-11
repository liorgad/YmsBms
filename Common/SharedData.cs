using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class SharedData
    {        
        public static SharedData Default { get; private set; }

        static SharedData()
        {
            Default = new SharedData();
        }

        protected SharedData()
        { }

        [DataMember]
        public ConcurrentDictionary<string, BatteryStatViewModel> BatteryPackContainer = new ConcurrentDictionary<string, BatteryStatViewModel>();

        public ClusterStatViewModel ClusterStatisticsVM { get; set; }

        public void Load(SynchronizationContext syncCtx)
        {
            if (File.Exists("data.json"))
            {
                using (var stream = new StreamReader("data.json"))
                {
                    //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SharedData));
                    //Default = (SharedData)ser.ReadObject(stream);

                    var jsonString = stream.ReadToEnd();
                    
                    Default = JsonConvert.DeserializeObject<SharedData>(jsonString);
                }
            }

            foreach (var item in Default.BatteryPackContainer.Values)
            {
                item.SyncCtx = syncCtx;
            }
        }


        public void Save()
        {
            using (var stream = new StreamWriter("data.json"))
            {
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SharedData));
                //ser.WriteObject(stream, Default);

                var output = JsonConvert.SerializeObject(Default);
                stream.Write(output);                
            }
        }

    }
}
