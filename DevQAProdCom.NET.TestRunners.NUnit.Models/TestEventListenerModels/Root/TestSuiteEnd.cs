using System.Xml.Serialization;
using DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Inner;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Root
{
    [XmlRoot(ElementName = "test-suite")]
    public class TestSuiteEnd
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("fullname")]
        public string FullName { get; set; }

        [XmlAttribute("runstate")]
        public string RunState { get; set; }

        [XmlAttribute("testcasecount")]
        public int TestCaseCount { get; set; }

        [XmlAttribute("result")]
        public string Result { get; set; }

        [XmlAttribute("start-time")]
        public DateTime StartTime { get; set; }

        [XmlAttribute("end-time")]
        public DateTime EndTime { get; set; }

        [XmlAttribute("duration")]
        public double Duration { get; set; }

        [XmlAttribute("total")]
        public int Total { get; set; }

        [XmlAttribute("passed")]
        public int Passed { get; set; }

        [XmlAttribute("failed")]
        public int Failed { get; set; }

        [XmlAttribute("warnings")]
        public int Warnings { get; set; }

        [XmlAttribute("inconclusive")]
        public int Inconclusive { get; set; }

        [XmlAttribute("skipped")]
        public int Skipped { get; set; }

        [XmlAttribute("asserts")]
        public int Asserts { get; set; }

        [XmlElement("environment")]
        public Inner.Environment Environment { get; set; }

        [XmlElement("settings")]
        public Settings Settings { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<Property> Properties { get; set; }

        [XmlElement("failure")]
        public Failure Failure { get; set; }

        [XmlElement("output")]
        public string Output { get; set; }

        [XmlElement("test-case")]
        public TestCaseEnd TestCase { get; set; }

        [XmlElement("test-suite")]
        public TestSuiteEnd InnerTestSuite { get; set; }
    }
}
