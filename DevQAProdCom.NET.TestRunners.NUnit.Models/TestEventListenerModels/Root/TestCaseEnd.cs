using System.Xml.Serialization;
using DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Inner;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Root
{
    [XmlRoot(ElementName = "test-case")]
    public class TestCaseEnd
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("fullname")]
        public string FullName { get; set; }

        [XmlAttribute("methodname")]
        public string MethodName { get; set; }

        [XmlAttribute("classname")]
        public string ClassName { get; set; }

        [XmlAttribute("runstate")]
        public string RunState { get; set; }

        [XmlAttribute("seed")]
        public string Seed { get; set; }

        [XmlAttribute("result")]
        public string Result { get; set; }

        [XmlAttribute("start-time")]
        public DateTime StartTime { get; set; }

        [XmlAttribute("end-time")]
        public DateTime EndTime { get; set; }

        [XmlAttribute("duration")]
        public double Duration { get; set; }

        [XmlAttribute("asserts")]
        public int Asserts { get; set; }

        [XmlAttribute("parentId")]
        public string ParentId { get; set; }

        [XmlElement("failure")]
        public Failure Failure { get; set; }

        [XmlElement("output")]
        public string Output { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<Property> Properties { get; set; }
    }
}
