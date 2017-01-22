using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Reflection;
using NLog;

namespace GenericParser
{
    public enum Command : byte
    {
        ProtectionData = 1,
        RealTimeData = 2,
        DataSetting = 5,
        FETOperation = 6,
        Version = 9
    }

    public enum CommandResponse : byte
    {
        RealTimeData = 0x82
    }

    public enum Version : byte
    {
        Version82 = 82
    }

    public class FrameFormat
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private string frameFormatStr;
        [ParserDefinition(0, 1)]
        public static char SOI { get; set; }

        [ParserDefinition(1, 2)]
        public byte Address { get; set; }

        [ParserDefinition(2, 2)]
        public byte Cmd { get; set; }

        [ParserDefinition(3, 2)]

        public byte Version { get; set; }

        [ParserDefinition(4, 4)]
        public ushort Length
        {
            get;
            set;
        }

        [ParserDefinition(5, -1, "Length")]
        public string Data { get; set; }

        [ParserDefinition(6, 2)]
        public byte CRC { get; set; }

        [ParserDefinition(7, 1)]
        public static char EOI { get; set; }

        public string AsString
        {
            get
            {
                frameFormatStr = this.ToString();
                return frameFormatStr;
            }
        }

        public override string ToString()
        {
            var length = CalculateLength();

            Length = Convert.ToUInt16(length);

            string tempStr = (string)GenericParser.Build<FrameFormat>(this);            

            if (string.IsNullOrWhiteSpace(tempStr))
            {
                return null;
            }

            var subStr = tempStr.TrimStart(new char[] { SOI }).TrimEnd(new char[] { EOI });

            subStr = subStr.Substring(0, subStr.Length - 2);

            var crc = CalculateCRC(subStr);

            var result = string.Format("{0}{1}{2}{3}", SOI, subStr, crc, EOI);



            return result;
        }

        public int CalculateLength()
        {
            try
            {
                var properties = this.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(ParserDefinitionAttribute), false));

                if (null == properties)
                {
                    return -1;
                }

                var orderedProperties = properties.OrderBy(p => ((ParserDefinitionAttribute)p.GetCustomAttribute(typeof(ParserDefinitionAttribute))).Index);


                if (null == orderedProperties)
                {
                    return -1;
                }

                int totalLength = 0;

                foreach (var item in orderedProperties)
                {
                    var lengthProp = item.GetCustomAttribute<ParserDefinitionAttribute>().DynamicLength;
                    var length = item.GetCustomAttribute<ParserDefinitionAttribute>().Length;
                    if (!string.IsNullOrWhiteSpace(lengthProp))
                    {
                        if (length == -1)
                        {
                            if (item.PropertyType.IsArray)
                            {
                                Array arr = (Array)item.GetValue(this);
                                totalLength += arr.Length;
                            }
                            else if (item.PropertyType == typeof(string))
                            {
                                string str = (string)item.GetValue(this);
                                if (!string.IsNullOrWhiteSpace(str))
                                {
                                    totalLength += str.Length;
                                }
                            }
                        }
                    }
                    else
                    {
                        totalLength += length;
                    }                   
                }

                return totalLength;

            }
            catch (Exception e)
            {
                logger.Error(e, "Error calculating length");
            }

            return -1;
        }

        public static string CalculateCRC(string str)
        {
            //crc cala verification method (C language)
            // i = length of string

            char[] strAsChars = str.ToCharArray();

            byte sum = 0;

            for (int i = 0; i < strAsChars.Length; i++)
            {
                sum += Convert.ToByte(strAsChars[i]);
            }

            return (sum ^= 0xFF).ToString("X2");
        }

        static FrameFormat()
        {
            SOI = ':';
            EOI = '~';
        }

        public FrameFormat()
        {
        }
    }
}
