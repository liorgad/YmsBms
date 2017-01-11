using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericParser
{    
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ParserDefinitionAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly int index;
        readonly int length;
        readonly string lengthIsInValue;

        // This is a positional argument
        public ParserDefinitionAttribute(int index,int length, string calculatedLength=null)
        {
            this.index = index;
            this.length = length; 
            if(!String.IsNullOrWhiteSpace(calculatedLength))
            {
                lengthIsInValue = calculatedLength;
            }        
        }

        public int Index
        {
            get { return this.index; }
        }

        public int Length
        {
            get { return this.length; }
        }  
        
        public string DynamicLength
        {
            get { return this.lengthIsInValue; }
        }     
    }   
}
