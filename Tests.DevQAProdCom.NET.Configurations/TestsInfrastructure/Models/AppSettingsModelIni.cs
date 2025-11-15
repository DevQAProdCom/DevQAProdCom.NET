using Microsoft.Extensions.Configuration;

namespace Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models
{
    internal class AppSettingsModelIni : AppSettingsModelBase0
    {
        #region CLASS ENTITY

        [ConfigurationKeyName("OtherData:ClassEntity_UserModelExtended")]
        public UserModelExtended ClassEntityUserModelExtended { get; set; }

        #endregion CLASS ENTITY

        #region IENUMERABLE

        [ConfigurationKeyName("IEnumerable")]
        public IEnumerable<IEnumerableModel> IEnumerable { get; set; }

        #endregion IENUMERABLE
    }
}
