using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigurationSerialization
    {
        public bool HasCluster { get; set; }

        public bool IsParallel { get; set; }

        public List<string> DefinedAddresses { get; set; }

        public List<string> Group1 { get; set; }

        public List<string> Group2 { get; set; }
    }
}
