using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Extentions
    {
        public static byte[] ToByteArray(this string str)
        {
            return Enumerable.Range(0, str.Length / 2).Select(x => Convert.ToByte(str.Substring(x * 2, 2), 16)).ToArray();
        }

        public static string ToEnumDescription(this Enum en) //ext method
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }

        public static string ToStringWithAtt<T>(this object obj)
        {
            Type objType = obj.GetType();
            var properties = objType.GetProperties().Where(prop => prop.IsDefined(typeof(T), false));
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}\t",obj.GetType().Name);
            

            foreach (var item in properties)
            {
                sb.Append(string.Format("[{0}]={1},",item.Name,item.GetValue(obj)));
            }

            return sb.ToString();
        }

        public static string ToStringAllProperties(this object obj)
        {
            Type objType = obj.GetType();
            var properties = objType.GetProperties();
            StringBuilder sb = new StringBuilder();

            foreach (var item in properties)
            {
                sb.Append(string.Format("[{0}]={1},", item.Name, item.GetValue(obj)));
            }

            return sb.ToString();
        }

    }
}
