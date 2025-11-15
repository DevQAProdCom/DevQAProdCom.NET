using System.Xml.Serialization;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Root
{
    [XmlRoot(ElementName = "start-suite")]
    public class TestSuiteStart
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("parentId")]
        public string ParentId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("fullname")]
        public string FullName { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("framework-version")]
        public string FrameworkVersion { get; set; }
    }
}
