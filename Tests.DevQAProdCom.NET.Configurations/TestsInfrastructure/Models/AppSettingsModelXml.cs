using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;

namespace Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models
{
    internal class AppSettingsModelXml : AppSettingsModelBase1
    {
        #region CLASS ENTITY

        [ConfigurationKeyName("OtherData:ClassEntity_UserModelExtended")]
        public UserModelExtendedXml ClassEntityUserModelExtended { get; set; }

        #endregion CLASS ENTITY

        #region IENUMERABLE

        [ConfigurationKeyName("IEnumerable:IEnumerableModel")]
        [XmlArray("IEnumerable")]
        [XmlArrayItem("IEnumerableModel")]
        public IEnumerable<IEnumerableModel> IEnumerable { get; set; }

        [ConfigurationKeyName("IEnumerable_Default_Empty:IEnumerableModel")]
        [XmlArray("IEnumerable_Default_Empty")]
        [XmlArrayItem("IEnumerableModel")]
        public IEnumerable<IEnumerableModel> IEnumerableDefaultEmpty { get; set; }

        [ConfigurationKeyName("IEnumerable_Default_Empty_With_Custom_Attributes:IEnumerableModel")]
        [XmlArray("IEnumerable_Default_Empty_With_Custom_Attributes")]
        [XmlArrayItem("IEnumerableModel")]
        public IEnumerable<IEnumerableModel> IEnumerableDefaultEmptyWithCustomAttributes { get; set; }

        [ConfigurationKeyName("IEnumerable_Default_Null:IEnumerableModel")]
        [XmlArray("IEnumerable_Default_Null")]
        [XmlArrayItem("IEnumerableModel")]
        public IEnumerable<IEnumerableModel> IEnumerableDefaultNull { get; set; }

        #endregion IENUMERABLE
    }
}
