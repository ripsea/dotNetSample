using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Api.Utilities
{
    public static class ExtensionMethod
    {

        public static String GetEfficientString(this String source)
        {
            String val = source != null ? source.Trim() : null;
            return String.IsNullOrEmpty(val) ? null : val;
        }

        public static Stream ConvertToXmlStream<T>(this T entData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            //docMsg.PreserveWhitespace = true;

            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, entData);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;

        }


        public static T ConvertTo<T>(this XmlDocument docMsg)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlNodeReader xnr = new XmlNodeReader(docMsg);
            T entData = (T)serializer.Deserialize(xnr);
            xnr.Close();
            return entData;
        }

        public static T ConvertTo<T>(this XmlNode docMsg)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlNodeReader xnr = new XmlNodeReader(docMsg);
            T entData = (T)serializer.Deserialize(xnr);
            xnr.Close();
            return entData;
        }
    }
}