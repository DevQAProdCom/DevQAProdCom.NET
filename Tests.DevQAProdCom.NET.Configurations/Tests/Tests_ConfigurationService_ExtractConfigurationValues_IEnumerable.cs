using DevQAProdCom.NET.Configurations.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Configurations.Constants;
using Tests.DevQAProdCom.NET.Configurations.TestData;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.BaseClasses;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Enumerations;
using Tests.DevQAProdCom.NET.Configurations.TestsInfrastructure.Models;

namespace Tests.DevQAProdCom.NET.Configurations.Tests
{
    internal class Tests_ConfigurationService_ExtractConfigurationValues_IEnumerable : BaseTest
    {
        #region Get

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.IEnumerable)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable_IEnumerableModel)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable)]
        public void Should_Get_Existing_In_Configurations_IEnumerable(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualIEnumerable = configContainer.Get<IEnumerable<IEnumerableModel>>(keyPath);

            //Assert
            actualIEnumerable.Should().BeEquivalentTo(ExpectedValues.GetIEnumerableModel(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.IEnumerable)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable_IEnumerableModel)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable)]
        public void Should_Get_Existing_In_Configurations_IEnumerable_Array(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            IEnumerable<IEnumerableModel>? actualIEnumerable = configContainer.Get<IEnumerableModel[]>(keyPath);

            //Assert
            actualIEnumerable.Should().BeEquivalentTo(ExpectedValues.GetIEnumerableModel(env));
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.IEnumerable_Default_Null)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable_Default_Null_IEnumerableModel)]
        public void Should_Get_Existing_In_Configurations_IEnumerable_Default_Null(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualIEnumerable = configContainer.Get<IEnumerable<IEnumerableModel>>(keyPath);

            //Assert
            actualIEnumerable.Should().BeNull();
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.IEnumerable_Default_Empty)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable_Default_Empty_IEnumerableModel)]
        public void Should_Get_Existing_In_Configurations_IEnumerable_Default_Empty(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            var actualIEnumerable = configContainer.Get<IEnumerable<IEnumerableModel>>(keyPath);

            //Assert
            actualIEnumerable.Should().BeEmpty();
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.IEnumerable_Default_Empty)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable_Default_Empty_IEnumerableModel)]
        public void Should_Get_Existing_In_Configurations_IEnumerable_Array_Default_Empty(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            IEnumerable<IEnumerableModel>? actualIEnumerable = configContainer.Get<IEnumerableModel[]>(keyPath);

            //Assert
            actualIEnumerable.Should().BeEmpty();
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_Get_Not_Existing_In_Configurations_IEnumerable_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Action act = () => configContainer.Get<IEnumerable<IEnumerableModel>>(keyPath);

            //Assert
            act.Should().Throw<Exception>().WithMessage(ExpectedValues.GetSectionNotFoundExceptionMessage(keyPath));
        }

        #endregion Get

        #region TryGet

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.IEnumerable)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable_IEnumerableModel)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable)]
        public void Should_TryGet_Existing_In_Configurations_IEnumerable(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out IEnumerable<IEnumerableModel>? actualIEnumerable);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualIEnumerable.Should().BeEquivalentTo(ExpectedValues.GetIEnumerableModel(env));
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.IEnumerable_Default_Null)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable_Default_Null_IEnumerableModel)]
        public void Should_TryGet_Existing_In_Configurations_IEnumerable_Default_Null(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out IEnumerable<IEnumerableModel>? actualIEnumerable);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualIEnumerable.Should().BeNull();
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.IEnumerable_Default_Empty)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.IEnumerable_Default_Empty_IEnumerableModel)]
        public void Should_TryGet_Existing_In_Configurations_IEnumerable_Default_Empty(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out IEnumerable<IEnumerableModel>? actualIEnumerable);

            //Assert
            using (new AssertionScope())
            {
                actualIsOperationSuccessful.Should().BeTrue();
                actualIEnumerable.Should().BeEmpty();
            }
        }

        [TestCase(Env.DEV, Const.Files.JSON_AppSettingsTestsDev, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.XML_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        [TestCase(Env.QA, Const.Files.INI_AppSettingsTestsQa, Const.ConfigurationKeyPaths.NotExistingConfigurationKeyPath)]
        public void Should_TryGet_Not_Existing_In_Configurations_IEnumerable_Not_Throw_Exception(Env env, string fileName, string keyPath)
        {
            //Arrange
            IConfigContainer configContainer = ArrangeIConfigContainer(env, out string tempDirectory, fileName).Object;

            //Act
            Func<bool> func = () => configContainer.TryGet(keyPath, out IEnumerable<IEnumerableModel>? actualIEnumerable);

            //Assert
            using (new AssertionScope())
            {
                func.Should().NotThrow();

                bool actualIsOperationSuccessful = configContainer.TryGet(keyPath, out IEnumerable<IEnumerableModel>? actualIEnumerable);
                actualIsOperationSuccessful.Should().BeFalse();
                actualIEnumerable.Should().BeNull();
            }
        }

        #endregion TryGet
    }
}
