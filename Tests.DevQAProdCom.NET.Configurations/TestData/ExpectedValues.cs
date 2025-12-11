using DevQAProdCom.NET.Global.Extensions;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Enumerations;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models;

namespace Tests.DevQAProdCom.NET.Configurations.TestData
{
    internal class ExpectedValues
    {
        #region Half-Dynamic Values - parameter specific. Same if parameter passed is the same. Exact parameters values may matter if hardcoded in configuration file.

        public static string GetStringValue(Env env) => $"StringValue {env}";
        public static string GetConnectionStringValue(Env env) => $"Server={env}-server;Database={env}-database;User Id={env}-user;Password={env}-password;";

        public static double GetNumberValue(Env env)
        {
            switch (env)
            {
                case Env.DEV:
                    return 0.01;
                case Env.QA:
                    return 0.02;
                default:
                    throw new Exception("Environment not supported");
            }
        }

        public static string GetIEnumerableValue(Env env, int number) => $"IEnumerable Value {number} {env}";
        public static UserModel GetClassEntityUserModel(Env env, int number) => new UserModel() { Email = $"user_{env}{number}@email.com", Password = $"password_{env}{number}" };

        public static List<IEnumerableModel> GetIEnumerableModel(Env env) => new List<IEnumerableModel>()
        {
            new IEnumerableModel() { Key = GetIEnumerableValue(env, 1), ClassEntity_UserModel = GetClassEntityUserModel(env, 1) },
            new IEnumerableModel() { Key = GetIEnumerableValue(env, 2), ClassEntity_UserModel = GetClassEntityUserModel(env, 2) },
        };

        public static UserModelExtended GetClassEntityUserModelExtended(Env env)
        {
            var userModel = GetClassEntityUserModel(env, 0);
            return new UserModelExtended()
            {
                Email = userModel.Email,
                Password = userModel.Password,
                IEnumerable = new List<IEnumerableModel>()
                {
                    new IEnumerableModel() { Key = GetIEnumerableValue(env, 3), ClassEntity_UserModel = GetClassEntityUserModel(env, 3) },
                    new IEnumerableModel() { Key = GetIEnumerableValue(env, 4), ClassEntity_UserModel = GetClassEntityUserModel(env, 4) },
                }
            };
        }

        public static UserModelExtendedXml GetClassEntityUserModelExtendedXml(Env env) => GetClassEntityUserModelExtended(env).CloneJson<UserModelExtendedXml>();

        public static string GetSectionNotFoundExceptionMessage(string keyPath)
        {
            return $"Unable to find within configurations any section using key path '{keyPath}'.";
        }

        public static string GetEnvironmentVariableNotFoundExceptionMessage(string name, StringComparison stringComparison)
        {
            return $"Environment variable '{name}' was not found. Comparison option used: 'StringComparison.{stringComparison}'.";

        }

        public static string GetUnableToConvertEnvironmentVariableExceptionMessage(string name, string envVariableValue, Type type)
        {
            return $"Unable to convert environment variable '{name}' with value '{envVariableValue}' to type '{type.FullName}'.";
        }

        public const string SingleWhiteSpace = " ";

        public static AppSettingsModelJson GetAppSettingsModel(Env env) => new AppSettingsModelJson()
        {
            StringValue = GetStringValue(env),
            StringValueEmpty = string.Empty,
            StringValueWhitespace = SingleWhiteSpace,
            StringValueDefaultNull = null,
            NumberValue = GetNumberValue(env),
            NumberValueDefaultZero = 0,
            NumberValueDefaultNull = null,
            BooleanValue = true,
            BooleanValueDefaultFalse = false,
            BooleanValueDefaultNull = null,
            ClassEntityUserModelExtended = GetClassEntityUserModelExtended(env),
            ClassEntityDefaultNull = null,
            IEnumerable = GetIEnumerableModel(env),
            IEnumerableDefaultEmpty = new List<IEnumerableModel>(),
            IEnumerableDefaultNull = null
        };

        public static AppSettingsModelXml GetAppSettingsModelXml(Env env) => GetAppSettingsModel(env).CloneJson<AppSettingsModelXml>();
        public static AppSettingsModelIni GetAppSettingsModelIni(Env env) => GetAppSettingsModel(env).CloneJson<AppSettingsModelIni>();

        #endregion Half-Dynamic Values - parameter specific. Same if parameter passed is the same. Exact parameters values may matter if hardcoded in configuration file.

        #region Dynamic Values - should be generated once and saved inside test as expected value

        public static string GetEnvironmentVariableName() => $"EnvironmentVariable_Name_{Guid.NewGuid()}";
        public static string GetEnvironmentVariableStringValue() => $"EnvironmentVariable_Value_{Guid.NewGuid()}";
        public static double GetEnvironmentVariableDoubleValue() => new Random().NextDouble();

        public static (string Name, string Value) GetEnvironmentVariableWithStringType() => (GetEnvironmentVariableName(), GetEnvironmentVariableStringValue());
        public static (string Name, double Value) GetEnvironmentVariableWithDoubleType() => (GetEnvironmentVariableName(), GetEnvironmentVariableDoubleValue());

        #endregion Dynamic Values - should be generated once and saved inside test as expected value
    }
}
