using Microsoft.Extensions.Configuration;

namespace Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models
{
    internal abstract class AppSettingsModelBase0
    {
        #region STRING 

        [ConfigurationKeyName("Level1:SubLevel1:StringValue")]
        public string StringValue { get; set; }

        [ConfigurationKeyName("Level1:SubLevel1:StringValue_Empty")]
        public string StringValueEmpty { get; set; }

        #endregion STRING

        #region NUMBER

        [ConfigurationKeyName("Level1:SubLevel1:NumberValue")]
        public double NumberValue { get; set; }

        [ConfigurationKeyName("Level1:SubLevel1:NumberValue_Default_Zero")]
        public double? NumberValueDefaultZero { get; set; }

        [ConfigurationKeyName("Level1:SubLevel1:NumberValue_Default_Null")]
        public double? NumberValueDefaultNull { get; set; }

        #endregion NUMBER

        #region BOOLEAN

        [ConfigurationKeyName("Level1:SubLevel1:BooleanValue")]
        public bool BooleanValue { get; set; }

        [ConfigurationKeyName("Level1:SubLevel1:BooleanValue_Default_False")]
        public bool? BooleanValueDefaultFalse { get; set; }

        [ConfigurationKeyName("Level1:SubLevel1:BooleanValue_Default_Null")]
        public bool? BooleanValueDefaultNull { get; set; }

        #endregion BOOLEAN
    }
}
