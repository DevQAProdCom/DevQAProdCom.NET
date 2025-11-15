using Microsoft.Extensions.Configuration;

namespace Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models
{
    internal class AppSettingsModelJson : AppSettingsModelBase1
    {
        #region CLASS ENTITY

        [ConfigurationKeyName("OtherData:ClassEntity_UserModelExtended")]
        public UserModelExtended ClassEntityUserModelExtended { get; set; }

        #endregion CLASS ENTITY

        #region IENUMERABLE

        [ConfigurationKeyName("IEnumerable")]
        public IEnumerable<IEnumerableModel> IEnumerable { get; set; }

        [ConfigurationKeyName("IEnumerable_Default_Empty")]
        public IEnumerable<IEnumerableModel> IEnumerableDefaultEmpty { get; set; }

        [ConfigurationKeyName("IEnumerable_Default_Null")]
        public IEnumerable<IEnumerableModel> IEnumerableDefaultNull { get; set; }

        #endregion IENUMERABLE
    }
}
