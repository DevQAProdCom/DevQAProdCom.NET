using System.Xml.Serialization;
using DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Inner;
using DevQAProdCom.NET.TestRunners.NUnit.Models.Types;

namespace DevQAProdCom.NET.TestRunners.NUnit.Models.TestEventListenerModels.Root
{
    [XmlRoot(ElementName = "test-run")]
    public class TestRunEnd
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

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

        [XmlAttribute("engine-version")]
        public string EngineVersion { get; set; }

        [XmlAttribute("clr-version")]
        public string ClrVersion { get; set; }

        [XmlAttribute("duration")]
        public double Duration { get; set; }

        [XmlElement("command-line")]
        public string CommandLine { get; set; }

        [XmlElement("filter")]
        public TestFilter Filter { get; set; }

        [XmlAttribute("start-time")]
        public string StartTimeString { get; set; }

        [XmlIgnore] public DateTime StartTime => new NUnitXmlDateTime(StartTimeString);

        [XmlAttribute("end-time")]
        public string EndTimeString { get; set; }

        [XmlIgnore] public DateTime EndTime => new NUnitXmlDateTime(EndTimeString);
    }
}
