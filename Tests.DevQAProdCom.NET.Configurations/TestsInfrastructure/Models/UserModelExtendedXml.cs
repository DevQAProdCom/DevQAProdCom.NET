using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;

namespace Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models
{
    internal class UserModelExtendedXml : UserModel
    {
        [ConfigurationKeyName("IEnumerable:IEnumerableModel")]
        [XmlArray("IEnumerable")]
        [XmlArrayItem("IEnumerableModel")]
        public IEnumerable<IEnumerableModel> IEnumerable { get; set; }
    }
}
