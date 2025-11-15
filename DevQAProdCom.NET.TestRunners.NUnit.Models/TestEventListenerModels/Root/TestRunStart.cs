using System.Xml.Serialization;
using DevQAProdCom.NET.TestRunners.NUnit.Models.Types;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Root
{
    [XmlRoot(ElementName = "start-run")]
    public class TestRunStart
    {
        [XmlAttribute("count")]
        public string Count { get; set; }

        [XmlAttribute("start-time")]
        public string StartTimeString { get; set; }

        [XmlIgnore] public DateTime StartTime => new NUnitXmlDateTime(StartTimeString);

        [XmlAttribute("engine-version")]
        public string EngineVersion { get; set; }

        [XmlAttribute("clr-version")]
        public string ClrVersion { get; set; }

        [XmlElement("command-line")]
        public string CommandLine { get; set; }
    }
}
