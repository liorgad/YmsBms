using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericParser
{
    public class FETMap
    {
        public byte SFET { get; set; } //0: DFET close
                                       //1: DFET open
                                       //2: CFET close
                                       //3: CFET open
    }
}
