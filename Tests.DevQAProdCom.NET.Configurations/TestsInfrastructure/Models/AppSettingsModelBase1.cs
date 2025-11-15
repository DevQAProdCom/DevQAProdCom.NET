using Microsoft.Extensions.Configuration;

namespace Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models
{
    internal abstract class AppSettingsModelBase1 : AppSettingsModelBase0
    {
        #region STRING 

        [ConfigurationKeyName("Level1:SubLevel1:StringValue_Whitespace")]
        public string StringValueWhitespace { get; set; }

        [ConfigurationKeyName("Level1:SubLevel1:StringValue_Default_Null")]
        public string StringValueDefaultNull { get; set; }

        #endregion STRING 

        #region CLASS ENTITY

        [ConfigurationKeyName("OtherData:ClassEntity_Default_Null")]
        public UserModel ClassEntityDefaultNull { get; set; }

        #endregion CLASS ENTITY
    }
}
