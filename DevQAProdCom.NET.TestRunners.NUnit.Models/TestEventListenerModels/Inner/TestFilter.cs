using System.Xml.Serialization;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Inner
{
    public class TestFilter
    {
        [XmlElement("test")]
        public string Test { get; set; }
    }
}
