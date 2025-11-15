using DevQAProdCom.NET.Configurations.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Configurations.Constants;
using Tests.DevQAProdCom.NET.Configurations.TestData;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.BaseClasses;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Enumerations;

namespace Tests.DevQAProdCom.NET.Configurations.Tests
{
    internal class Tests_ConfigurationService_ExtractConfigurationValues_Number : BaseTest
    {
        #region Get

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NumberValue)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue)]
        public void Should_Get_Existing_In_Configurations_NumberValue(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualValue = configContainer.Get<double>(keyPath);

            //Assert
            actualValue.Should().Be(ExpectedValues.GetNumberValue(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NumberValue_Default_Zero)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue_Default_Zero)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue_Default_Zero)]
        public void Should_Get_Existing_In_Configurations_NumberValue_Default_Zero(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualValue = configContainer.Get<int>(keyPath);

            //Assert
            actualValue.Should().Be(0);
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NumberValue_Default_Null)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue_Default_Null)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue_Default_Null)]
        public void Should_Get_Existing_In_Configurations_NumberValue_Default_Null(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualValue = configContainer.Get<float?>(keyPath);

            //Assert
            actualValue.Should().BeNull();
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_Get_Not_Existing_In_Configurations_NumberValue_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Action act = () => configContainer.Get<double>(keyPath);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetSectionNotFoundExceptionMessage(keyPath));
        }

        #endregion Get

        #region TryGet

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NumberValue)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue)]
        public void Should_TryGet_Existing_In_Configurations_NumberValue(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out double actualValue);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().Be(ExpectedValues.GetNumberValue(env));
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NumberValue_Default_Zero)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue_Default_Zero)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue_Default_Zero)]
        public void Should_TryGet_Existing_In_Configurations_NumberValue_Default_Zero(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out int actualValue);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualValue.Should().Be(0);
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NumberValue_Default_Null)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue_Default_Null)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NumberValue_Default_Null)]
        public void Should_TryGet_Existing_In_Configurations_NumberValue_Default_Null(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out float? actualValue);

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
        public void Should_TryGet_Not_Existing_In_Configurations_NumberValue_Not_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Func<bool> func = () => configContainer.TryGet(keyPath, out double actualValue);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = configContainer.TryGet(Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath, out double actualValue);
                actualIsOperationSuccessful.Should().BeFalse();
                actualValue.Should().Be(default);
            }
        }

        #endregion TryGet
    }
}
