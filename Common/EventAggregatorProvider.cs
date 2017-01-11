using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EventAggregatorProvider
    {
        public static IEventAggregator EventAggregator { get; private set; }        

        static EventAggregatorProvider()
        {
            EventAggregator = new EventAggregator();            
        }
    }
}
