using DevQAProdCom.NET.Configurations.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Configurations.Constants;
using Tests.DevQAProdCom.NET.Configurations.TestData;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.BaseClasses;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Enumerations;

namespace Tests.DevQAProdCom.NET.Configurations.Tests
{
    internal class Tests_ConfigurationService_ExtractConfigurationValues_ConnectionString : BaseTest
    {
        #region GetConnectionString

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.DefaultConnectionString)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.DefaultConnectionString)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.DefaultConnectionString)]
        public void Should_GetConnectionString_Existing_In_Configurations(Env env, string fileName, string name)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            string actualValue = configContainer.GetConnectionString(name);

            //Assert
            actualValue.Should().BeEquivalentTo(ExpectedValues.GetConnectionStringValue(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConnectionString)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConnectionString)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConnectionString)]
        public void Should_GetConnectionString_Not_Existing_In_Configurations_Throw_Exception(Env env, string fileName, string name)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Action act = () => configContainer.GetConnectionString(name);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetSectionNotFoundExceptionMessage($"{Const.ConfigurationKeyPaths.ConnectionStrings}:{name}"));
        }

        #endregion GetConnectionString

        #region TryGetConnectionString

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.DefaultConnectionString)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.DefaultConnectionString)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.DefaultConnectionString)]
        public void Should_TryGetConnectionString_Existing_In_Configurations(Env env, string fileName, string name)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGetConnectionString(name, out string? actualValue);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().BeEquivalentTo(ExpectedValues.GetConnectionStringValue(env));
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConnectionString)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConnectionString)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConnectionString)]
        public void Should_TryGetConnectionString_Not_Existing_In_Configurations_Not_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Func<bool> func = () => configContainer.TryGetConnectionString(keyPath, out string? actualValue);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = configContainer.TryGet(Const.ConfigurationKeyPaths.NotExistingConnectionString, out string? actualValue);
                actualIsOperationSuccessful.Should().BeFalse();
                actualValue.Should().Be(null);
            }
        }

        #endregion TryGetConnectionString
    }
}
