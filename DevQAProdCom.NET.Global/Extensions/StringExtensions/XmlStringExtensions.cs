using System.Xml;
using System.Xml.Serialization;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;

namespace DevQAProdCom.NET.Global.Extensions.StringExtensions
{
    public static class XmlStringExtensions
    {
        public static T FromXml<T>(this string xmlContent, XmlReaderSettings? settings = null, ILogger? logger = null)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xmlContent))
            using (XmlReader xmlReader = XmlReader.Create(reader, settings))
            {
                try
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
                catch (Exception ex)
                {
                    logger?.Error($"Unable to deserialize to type '{typeof(T).FullName}' string '{xmlContent}'. Error message: '{ex.Message}'.", typeof(T).FullName, xmlContent, ex.Message);
                    throw;
                }
            }
        }
    }
}
