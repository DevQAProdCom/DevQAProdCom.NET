using System.Xml.Serialization;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Inner
{
    public class Failure
    {
        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("stack-trace")]
        public string StackTrace { get; set; }
    }
}
