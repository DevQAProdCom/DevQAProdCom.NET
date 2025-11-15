using System.Xml.Serialization;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Root
{
    [XmlRoot(ElementName = "start-test")]
    public class TestCaseStart
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

        [XmlAttribute("classname")]
        public string ClassName { get; set; }

        [XmlAttribute("methodname")]
        public string MethodName { get; set; }
    }
}
