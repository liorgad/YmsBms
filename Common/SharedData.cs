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

        public const string CLUSTER = "cluster";

        static SharedData()
        {
            Default = new SharedData();
        }

        protected SharedData()
        { }

        [DataMember]
        public ConcurrentDictionary<string, BatteryStatViewModel> BatteryPackContainer = new ConcurrentDictionary<string, BatteryStatViewModel>();

        public ClusterStatViewModel ClusterStatisticsVM { get; set; }

        public ConfigurationSerialization Load(SynchronizationContext syncCtx)
        {
            ConfigurationSerialization confSer;
            if (File.Exists("data.json"))
            {
                using (var stream = new StreamReader("data.json"))
                {
                    //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SharedData));
                    //Default = (SharedData)ser.ReadObject(stream);

                    var jsonString = stream.ReadToEnd();
                    
                    confSer = JsonConvert.DeserializeObject<ConfigurationSerialization>(jsonString);
                }

                this.BatteryPackContainer.Clear();

                return confSer;
            }

            return null;
        }


        public void Save()
        {
            using (var stream = new StreamWriter("data.json"))
            {
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SharedData));
                //ser.WriteObject(stream, Default);

                var hasCluster = this.BatteryPackContainer.Keys.Contains(CLUSTER);
                var isParallel = hasCluster ? this.BatteryPackContainer[CLUSTER] is SeriesStatViewModel : false;

                ConfigurationSerialization ser = new ConfigurationSerialization()
                {
                    DefinedAddresses = this.BatteryPackContainer.Where(bs => bs.Key != CLUSTER)?.Select(bs => bs.Value.Address)?.ToList(),
                    HasCluster = hasCluster,
                    IsParallel = isParallel,
                    Group1 = isParallel ? ((SeriesStatViewModel)this.BatteryPackContainer[CLUSTER]).SeriesBatteriesAddresses.Select(b => b.Address).ToList() :
                    ((ClusterStatViewModel)this.BatteryPackContainer[CLUSTER]).SeriesVm[0].SeriesBatteriesAddresses.Select(b => b.Address).ToList(),
                    Group2 = isParallel ? null : ((ClusterStatViewModel)this.BatteryPackContainer[CLUSTER]).SeriesVm[1].SeriesBatteriesAddresses.Select(b => b.Address).ToList()
                };

                var output = JsonConvert.SerializeObject(ser);
                stream.Write(output);                
            }
        }

    }
}
