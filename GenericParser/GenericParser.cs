using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericParser
{
    public enum DataType
    {
        BITS,
        BYTES,
        ASCII,
        ASCII_HEX
    }

    public static class GenericParser
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static T Parse<T>(string data,DataType dataType=DataType.ASCII_HEX) where T : class
        {
            try
            {
                var properties = typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(ParserDefinitionAttribute), false));
                
                if(null == properties)
                {
                    return default(T);
                }

                var orderedProperties = properties.OrderBy(p => ((ParserDefinitionAttribute)p.GetCustomAttribute(typeof(ParserDefinitionAttribute))).Index);

                //var dynamicLengthProperties = orderedProperties.Where(p => p.GetCustomAttribute<ParserDefinitionAttribute>().Index == -1);

                if (null == orderedProperties)
                {
                    return default(T);
                }

                var obj = Activator.CreateInstance<T>();

                switch (dataType)
                {
                    case DataType.BITS:
                        break;
                    case DataType.BYTES:
                        break;
                    case DataType.ASCII:
                        break;
                    case DataType.ASCII_HEX:
                        var resultDictionary = ParseASCIIHex(data,orderedProperties);  
                        
                        if(null == resultDictionary)
                        {
                            return null;
                        }
                                              
                        foreach (var item in resultDictionary)
                        {
                            var property = orderedProperties.First(p => p.Name == item.Key);
                            var propertyType = property.PropertyType;
                            if(propertyType == typeof(char))
                            {
                                property.SetValue(obj, Convert.ToChar(item.Value));
                            }
                            else if (propertyType == typeof(byte))
                            {
                               property.SetValue(obj,byte.Parse(item.Value, System.Globalization.NumberStyles.HexNumber));
                            }
                            else if(propertyType == typeof(Int16))
                            {
                                property.SetValue(obj, Int16.Parse(item.Value, System.Globalization.NumberStyles.HexNumber));
                            }
                            else if(propertyType == typeof(Int32))
                            {
                                property.SetValue(obj, Int32.Parse(item.Value, System.Globalization.NumberStyles.HexNumber));
                            }
                            else if(propertyType == typeof(ushort))
                            {
                                property.SetValue(obj, ushort.Parse(item.Value, System.Globalization.NumberStyles.HexNumber));
                            }
                            else if (propertyType == typeof(ulong))
                            {
                                property.SetValue(obj, ulong.Parse(item.Value, System.Globalization.NumberStyles.HexNumber));
                            }
                            else if (propertyType == typeof(uint))
                            {
                                property.SetValue(obj, uint.Parse(item.Value, System.Globalization.NumberStyles.HexNumber));
                            }
                            else if(propertyType == typeof(byte[]))
                            {
                                var val = Enumerable.Range(0, item.Value.Length / 2).Select(x => Convert.ToByte(item.Value.Substring(x * 2, 2), 16)).ToArray();
                                property.SetValue(obj, val);
                            }
                            else if (propertyType == typeof(ushort[]))
                            {
                                var val = Enumerable.Range(0, item.Value.Length / 4).Select(x => Convert.ToUInt16(item.Value.Substring(x * 4, 4), 16)).ToArray();
                                property.SetValue(obj, val);
                            }
                            else if(propertyType == typeof(string))
                            {
                                property.SetValue(obj,item.Value);
                            }
                        }
                        break;
                    default:
                        break;
                }

                return obj;

            }
            catch (Exception e)
            {
                logger.Error(e, "Error in parse, type " + typeof(T).Name);
            }

            return null;
        }

        public static object Build<T>(T obj,DataType resultDataType = DataType.ASCII_HEX)
        {
            try
            {
                var properties = typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(ParserDefinitionAttribute), false));

                if (null == properties)
                {
                    return default(T);
                }

                var orderedProperties = properties.OrderBy(p => ((ParserDefinitionAttribute)p.GetCustomAttribute(typeof(ParserDefinitionAttribute))).Index);


                if (null == orderedProperties)
                {
                    return default(T);
                }

                var resultList = new List<string>();

                foreach (var item in orderedProperties)
                {
                    var val = item.GetValue(obj);
                    var type = item.PropertyType;
                    
                    if (type == typeof(char))
                    {
                        var str = Convert.ToString(val);
                        //Debug.WriteLine(str);
                        resultList.Add(str);
                    }
                    else if (type == typeof(byte))
                    {
                        var str = ((byte)val).ToString("X2");
                        //Debug.WriteLine(str);
                        resultList.Add(str);
                    }
                    else if (type == typeof(ushort) || type == typeof(short))
                    {
                        var str = ((ushort)val).ToString("X4");
                        resultList.Add(str);
                    }
                    else if(type == typeof(string))
                    {
                        var str = (string)val != null ? (string)val : string.Empty;
                        //Debug.WriteLine(str);
                        StringBuilder sb = new StringBuilder(str);
                        resultList.Add(sb.ToString());
                    }
                    else if(type == typeof(ulong))
                    {
                        var formatString = string.Format("X{0}", item.GetCustomAttribute<ParserDefinitionAttribute>().Length);
                        var str = ((ulong)val).ToString(formatString);
                        //Debug.WriteLine(str);
                        resultList.Add(str);
                    }
                    else if(type == typeof(ushort[]))
                    {
                        var arrAsStr = ((ushort[])val).Select(v => v.ToString("X4")).Aggregate((v1, v2) => string.Format("{0}{1}",v1,v2));
                        //Debug.WriteLine(arrAsStr);
                        resultList.Add(arrAsStr);
                    }
                    else if (type == typeof(byte[]))
                    {
                        var arrAsStr = ((byte[])val).Select(v => v.ToString("X2")).Aggregate((v1, v2) => string.Format("{0}{1}", v1, v2));
                        //Debug.WriteLine(arrAsStr);
                        resultList.Add(arrAsStr);
                    }
                }

                var res = resultList.Aggregate((s1, s2) => s1 + s2);
                //Debug.WriteLine(res);
                return res;
            }
            catch(Exception e)
            {
                logger.Error(e, "Error Building from object of type " + obj.GetType().Name);
            }

            return null;
        }

        private static IDictionary<string,string> ParseASCIIHex(string data, IOrderedEnumerable<PropertyInfo> orderedProperties)
        {
            try
            {
                var resultDictionary = new Dictionary<string, string>();

                int index = 0;

                var totalKnownLength = orderedProperties.Where(p => p.GetCustomAttribute<ParserDefinitionAttribute>().Length != -1)
                    .Select(p => p.GetCustomAttribute<ParserDefinitionAttribute>().Length).Sum();


                foreach (var item in orderedProperties)
                {
                    var lengthProp = item.GetCustomAttribute<ParserDefinitionAttribute>().DynamicLength;
                    var length = item.GetCustomAttribute<ParserDefinitionAttribute>().Length;
                    if (!string.IsNullOrWhiteSpace(lengthProp))
                    {
                        if (length == -1)
                        {
                            length = int.Parse(resultDictionary[lengthProp], System.Globalization.NumberStyles.HexNumber) - totalKnownLength;
                        }
                        else
                        {
                            length *= int.Parse(resultDictionary[lengthProp], System.Globalization.NumberStyles.HexNumber);
                        }
                    }

                    if (index + length > data.Length)
                    {
                        return null;
                    }

                    resultDictionary.Add(item.Name, data.Substring(index, length));
                    index += length;
                }

                return resultDictionary;
            }
            catch(Exception e)
            {
                logger.Error(e, "Error parsing asci string, received string = " + data);
            }

            return null;
        }
    }
}
