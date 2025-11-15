namespace Tests.DevQAProdCom.NET.Configurations.Constants
{
    internal partial class Const
    {
        public static class ConfigurationKeyPaths
        {
            public const string BooleanValue = "Level1:SubLevel1:BooleanValue";
            public const string BooleanValue_Default_False = "Level1:SubLevel1:BooleanValue_Default_False";
            public const string BooleanValue_Default_Null = "Level1:SubLevel1:BooleanValue_Default_Null";

            public const string ClassEntity_UserModelExtended = "OtherData:ClassEntity_UserModelExtended";
            public const string ClassEntity_Default_Null = "OtherData:ClassEntity_Default_Null";

            public const string IEnumerable = "IEnumerable";
            public const string IEnumerable_IEnumerableModel = "IEnumerable:IEnumerableModel";
            public const string IEnumerable_Default_Null = "IEnumerable_Default_Null";
            public const string IEnumerable_Default_Null_IEnumerableModel = "IEnumerable_Default_Null:IEnumerableModel";
            public const string IEnumerable_Default_Empty = "IEnumerable_Default_Empty";
            public const string IEnumerable_Default_Empty_IEnumerableModel = "IEnumerable_Default_Empty:IEnumerableModel";

            public const string NumberValue = "Level1:SubLevel1:NumberValue";
            public const string NumberValue_Default_Zero = "Level1:SubLevel1:NumberValue_Default_Zero";
            public const string NumberValue_Default_Null = "Level1:SubLevel1:NumberValue_Default_Null";

            public const string StringValue = "Level1:SubLevel1:StringValue";
            public const string StringValue_Default_Null = "Level1:SubLevel1:StringValue_Default_Null";
            public const string StringValue_Empty = "Level1:SubLevel1:StringValue_Empty";
            public const string StringValue_Whitespace = "Level1:SubLevel1:StringValue_Whitespace";

            public const string ConnectionStrings = "ConnectionStrings";
            public const string DefaultConnectionString = "DefaultConnectionString";
            public const string NotExistingConnectionString = "NotExistingConnectionString";

            public const string NotExistingConfigurationKeyPath = "Not:Existing:Configuration:KeyPath";
        }
    }
}
