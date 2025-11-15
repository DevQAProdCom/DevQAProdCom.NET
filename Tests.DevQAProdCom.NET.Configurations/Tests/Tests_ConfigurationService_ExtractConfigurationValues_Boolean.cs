using DevQAProdCom.NET.Configurations.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Configurations.Constants;
using Tests.DevQAProdCom.NET.Configurations.TestData;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.BaseClasses;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Enumerations;

namespace Tests.DevQAProdCom.NET.Configurations.Tests
{
    internal class Tests_ConfigurationService_ExtractConfigurationValues_Boolean : BaseTest
    {
        #region Get

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.BooleanValue)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue)]
        public void Should_Get_Existing_In_Configurations_BooleanValue(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualValue = configContainer.Get<bool>(keyPath);

            //Assert
            actualValue.Should().BeTrue();
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.BooleanValue_Default_False)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue_Default_False)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue_Default_False)]
        public void Should_Get_Existing_In_Configurations_BooleanValue_Default_False(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualValue = configContainer.Get<bool>(keyPath);

            //Assert
            actualValue.Should().BeFalse();
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.BooleanValue_Default_Null)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue_Default_Null)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue_Default_Null)]
        public void Should_Get_Existing_In_Configurations_BooleanValue_Default_Null(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualValue = configContainer.Get<bool?>(keyPath);

            //Assert
            actualValue.Should().BeNull();
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_Get_Not_Existing_In_Configurations_BooleanValue_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Action act = () => configContainer.Get<bool>(keyPath);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetSectionNotFoundExceptionMessage(keyPath));
        }

        #endregion Get

        #region TryGet

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.BooleanValue)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue)]
        public void Should_TryGet_Existing_In_Configurations_BooleanValue(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out bool actualValue);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().BeTrue();
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.BooleanValue_Default_False)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue_Default_False)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue_Default_False)]
        public void Should_TryGet_Existing_In_Configurations_BooleanValue_Default_False(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out bool actualValue);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().BeFalse();
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.BooleanValue_Default_Null)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue_Default_Null)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.BooleanValue_Default_Null)]
        public void Should_TryGet_Existing_In_Configurations_BooleanValue_Default_Null(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out bool? actualValue);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().BeNull();
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_TryGet_Not_Existing_In_Configurations_BooleanValue_Not_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Func<bool> func = () => configContainer.TryGet(keyPath, out bool actualValue);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out bool actualValue);
                actualIsOperationSuccessful.Should().BeFalse();
                actualValue.Should().BeFalse();
            }
        }

        #endregion TryGet
    }
}
