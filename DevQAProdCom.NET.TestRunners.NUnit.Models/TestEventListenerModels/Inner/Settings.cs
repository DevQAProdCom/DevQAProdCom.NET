using System.Xml.Serialization;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Inner
{
    public class Settings
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
