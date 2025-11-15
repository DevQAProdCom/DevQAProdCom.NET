using System.Text;
using System.Text.Json;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Hooks;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Models;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Models;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.TestData;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Tests
{

    [Order(1)]
    internal class LoggingRecordsPresenceTestsOnPreconditionsTestsSuite1 : BaseTest
    {
        #region [SuiteStart]

        [Test]
        public void Should_Log_Contain_Suite1_SuiteStart_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.TestSuiteName == SharedTestConstants.Suite1FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteStart.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestSuiteName == SharedTestConstants.Suite1FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteStart.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.LogEntryOrderNumber)
                    .Excluding(x => x.StartTime));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
                actualFullLogRecord?.StartTime.Should().MatchRegex(DateTimeRegex);
            }
        }

        #endregion [SuiteStart]

        #region [OneTimeSetUp]

        [Test]
        public void Should_Log_Contain_Suite1_PerScenarioBaseTest_OneTimeSetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a496a7f4 && x.TestSuiteName == SharedTestConstants.Suite1FullNameWithPreconditionTests);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a496a7f4 && x.TestSuiteName == SharedTestConstants.Suite1FullNameWithPreconditionTests);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.LogEntryOrderNumber)
                        .Excluding(x => x.TestSuiteId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
                actualFullLogRecord?.TestSuiteId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite1_ClassWithTest_OneTimeSetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3c0d3be);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3c0d3be);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                      .Excluding(x => x.ThreadId)
                      .Excluding(x => x.LogEntryOrderNumber)
                      .Excluding(x => x.TestSuiteId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestSuiteId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
            }
        }

        #endregion [OneTimeSetUp]

        #region Suite1_Test_Info

        #region [TestStart] Suite1_Test_Info

        [Test]
        public void Should_Log_Contain_Suite1_Test_Info_TestStart_Data()
        {
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.TestMethodName == SharedTestConstants.Suite1TestInfo && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestStart.ToString());
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite1TestInfo && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestStart.ToString());

            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.StartTime)
                        .Excluding(x => x.LogRecordId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.StartTime.Should().MatchRegex(DateTimeRegex);
            }
        }

        #endregion [TestStart] Suite1_Test_Info

        #region [SetUp] Suite1_Test_Info 

        [Test]
        public void Should_Log_Contain_Suite1_Test_Info_PerScenarioBaseTest_SetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3eea371 && x.TestMethodName == SharedTestConstants.Suite1TestInfo);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3eea371 && x.TestMethodName == SharedTestConstants.Suite1TestInfo);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite1_Test_Info_ClassWithTest_SetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.b1f3c1f5 && x.TestMethodName == SharedTestConstants.Suite1TestInfo);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.b1f3c1f5 && x.TestMethodName == SharedTestConstants.Suite1TestInfo);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [SetUp] Suite1_Test_Info

        #region [Execution] Suite1_Test_Info 

        [Test]
        public void Should_Log_Contain_Suite1_Test_Info_TestExecution_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.f2a06559);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.f2a06559);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [Execution] Suite1_Test_Info

        #region [TearDown] Suite1_Test_Info 

        [Test]
        public void Should_Log_Contain_Suite1_Test_Info_ClassWithTest_TearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.afb88fb7 && x.TestMethodName == SharedTestConstants.Suite1TestInfo);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.afb88fb7 && x.TestMethodName == SharedTestConstants.Suite1TestInfo);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite1_Test_Info_PerScenarioBaseTest_TearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a0f4db90 && x.TestMethodName == SharedTestConstants.Suite1TestInfo);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a0f4db90 && x.TestMethodName == SharedTestConstants.Suite1TestInfo);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [TearDown] Suite1_Test_Info

        #region [TestExecutionEnd] Suite1_Test_Info

        [Test]
        public void Should_Log_Contain_Suite1_Test_Info_TestExecutionEnd_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.TestMethodName == SharedTestConstants.Suite1TestInfo
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite1TestInfo
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.EndTime)
                        .Excluding(x => x.Duration));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.EndTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.Duration.Should().MatchRegex(TimeSpanRegex);
            }
        }

        #endregion [TestExecutionEnd] Suite1_Test_Info

        #endregion Suite1_Test_Info

        #region Suite1_TestCase_Debug

        #region [TestStart] Suite1_TestCase_Debug

        [Test]
        public void Should_Log_Contain_Suite1_TestCase_Debug_TestStart_Data()
        {
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestStart.ToString());
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestStart.ToString());

            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.StartTime)
                        .Excluding(x => x.LogRecordId)
                        .Excluding(x => x.TestArguments));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.StartTime.Should().MatchRegex(DateTimeRegex);

                actualFullLogRecord?.TestArguments.Count.Should().Be(3);

                if (actualFullLogRecord?.TestArguments[SharedTestConstants.IntParameterName] is JsonElement actualIntParameterName)
                    actualIntParameterName.GetInt32().Should().Be(Convert.ToInt32(expectedLogRecord.TestArguments[SharedTestConstants.IntParameterName]));
                if (actualFullLogRecord?.TestArguments[SharedTestConstants.StringParameterName] is JsonElement actualStringParameterName)
                    actualStringParameterName.GetString().Should().Be(expectedLogRecord.TestArguments[SharedTestConstants.StringParameterName].ToString());
                if (actualFullLogRecord?.TestArguments[SharedTestConstants.DoubleParameterName] is JsonElement actualDoubleParameterName)
                    actualDoubleParameterName.GetDouble().Should().Be(Convert.ToDouble(expectedLogRecord.TestArguments[SharedTestConstants.DoubleParameterName]));
            }

            Assert.Multiple(() =>
            {
                if (!(actualFullLogRecord?.TestArguments[SharedTestConstants.IntParameterName] is JsonElement))
                    Assert.Fail($"Actual 'IntParameterName' object is not of 'JsonElement' type, but of not supported by test: {actualFullLogRecord?.TestArguments[SharedTestConstants.IntParameterName].GetType()}.");
                if (!(actualFullLogRecord?.TestArguments[SharedTestConstants.StringParameterName] is JsonElement))
                    Assert.Fail($"Actual 'StringParameterName' object is not of 'JsonElement' type, but of not supported by test: {actualFullLogRecord?.TestArguments[SharedTestConstants.StringParameterName].GetType()}.");
                if (!(actualFullLogRecord?.TestArguments[SharedTestConstants.DoubleParameterName] is JsonElement))
                    Assert.Fail($"Actual 'DoubleParameterName' object is not of 'JsonElement' type, but of not supported by test: {actualFullLogRecord?.TestArguments[SharedTestConstants.DoubleParameterName].GetType()}.");
            });
        }

        #endregion [TestStart] Suite1_TestCase_Debug

        #region [SetUp] Suite1_TestCase_Debug 

        [Test]
        public void Should_Log_Contain_Suite1_TestCase_Debug_PerScenarioBaseTest_SetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3eea371 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3eea371 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite1_TestCase_Debug_ClassWithTest_SetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.b1f3c1f5 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.b1f3c1f5 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [SetUp] Suite1_TestCase_Debug

        #region [Execution] Suite1_TestCase_Debug

        [Test]
        public void Should_Log_Contain_Suite1_TestCase_Debug_TestExecution_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.aa4524b4);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.aa4524b4);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [Execution] Suite1_TestCase_Debug

        #region [TearDown] Suite1_TestCase_Debug 

        [Test]
        public void Should_Log_Contain_Suite1_TestCase_Debug_ClassWithTest_TearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.afb88fb7 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.afb88fb7 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite1_TestCase_Debug_PerScenarioBaseTest_TearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a0f4db90 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a0f4db90 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [TearDown] Suite1_TestCase_Debug

        #region [TestExecutionEnd] Suite1_TestCase_Debug

        [Test]
        public void Should_Log_Contain_Suite1_TestCase_Debug_TestExecutionEnd_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.EndTime)
                        .Excluding(x => x.Duration));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.EndTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.Duration.Should().MatchRegex(TimeSpanRegex);
            }
        }

        #endregion [TestExecutionEnd] Suite1_TestCase_Debug

        #endregion Suite1_TestCase_Debug

        #region Suite1_TestCaseSource_Verbose

        #region [TestStart] Suite1_TestCaseSource_Verbose

        [Test]
        public void Should_Log_Contain_Suite1_TestCaseSource_Verbose_TestStart_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestStart.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestStart.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.StartTime)
                        .Excluding(x => x.LogRecordId)
                        .Excluding(x => x.TestArguments));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.StartTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.TestArguments.Count.Should().Be(2);

                if (actualFullLogRecord?.TestArguments[SharedTestConstants.IntParameterName] is JsonElement actualIntParameterName)
                    actualIntParameterName.GetInt32().Should().Be(Convert.ToInt32(expectedLogRecord.TestArguments[SharedTestConstants.IntParameterName]));

                if (actualFullLogRecord?.TestArguments[SharedTestConstants.Parameters] is JsonElement actualStringParameterName)
                {
                    var actualParameters = JsonSerializer.Deserialize<Parameters>(actualStringParameterName.GetRawText());
                    actualParameters.Should().BeEquivalentTo(expectedLogRecord.TestArguments[SharedTestConstants.Parameters]);
                }
            }

            Assert.Multiple(() =>
            {
                if (!(actualFullLogRecord?.TestArguments[SharedTestConstants.IntParameterName] is JsonElement))
                    Assert.Fail($"Actual 'IntParameterName' object is not of 'JsonElement' type, but of not supported by test: {actualFullLogRecord?.TestArguments[SharedTestConstants.IntParameterName].GetType()}.");
                if (!(actualFullLogRecord?.TestArguments[SharedTestConstants.Parameters] is JsonElement))
                    Assert.Fail($"Actual 'Parameters' object is not of 'JsonElement' type, but of not supported by test: {actualFullLogRecord?.TestArguments[SharedTestConstants.Parameters].GetType()}.");
            });
        }

        #endregion [TestStart] Suite1_TestCaseSource_Verbose

        #region [SetUp] Suite1_TestCaseSource_Verbose

        [Test]
        public void Should_Log_Contain_Suite1_TestCaseSource_Verbose_PerScenarioBaseTest_SetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3eea371 && x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3eea371 && x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite1_TestCaseSource_Verbose_ClassWithTest_SetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.b1f3c1f5 && x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.b1f3c1f5 && x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [SetUp] Suite1_TestCaseSource_Verbose

        #region [Execution] Suite1_TestCaseSource_Verbose

        [Test]
        public void Should_Log_Contain_Suite1_TestCaseSource_Verbose_TestExecution_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3a9c958);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a3a9c958);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [Execution] Suite1_TestCaseSource_Verbose

        #region [TearDown] Suite1_TestCaseSource_Verbose

        [Test]
        public void Should_Log_Contain_Suite1_TestCaseSource_Verbose_ClassWithTest_TearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.afb88fb7 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.afb88fb7 && x.TestMethodName == SharedTestConstants.Suite1TestCaseDebug);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite1_TestCaseSource_Verbose_PerScenarioBaseTest_TearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a0f4db90 && x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a0f4db90 && x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [TearDown] Suite1_TestCaseSource_Verbose

        #region [TestExecutionEnd] Suite1_TestCaseSource_Verbose

        [Test]
        public void Should_Log_Contain_Suite1_TestCaseSource_Verbose_TestExecutionEnd_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestMethodName == SharedTestConstants.Suite1TestCaseSourceVerbose
                && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.TestExecutionEnd.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestId)
                        .Excluding(x => x.EndTime)
                        .Excluding(x => x.Duration));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.EndTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.Duration.Should().MatchRegex(TimeSpanRegex);
            }
        }

        #endregion [TestExecutionEnd] Suite1_TestCaseSource_Verbose

        #endregion Suite1_TestCaseSource_Verbose

        #region [OneTimeTearDown]

        [Test]
        public void Should_Log_Contain_Suite1_ClassWithTest_OneTimeTearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a12a608c);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a12a608c);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.TestSuiteId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestSuiteId.Should().MatchRegex(IdPattern);
            }
        }

        [Test]
        public void Should_Log_Contain_Suite1_PerScenarioBaseTest_OneTimeTearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a2320c90 && x.TestSuiteName == SharedTestConstants.Suite1FullNameWithPreconditionTests);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a2320c90 && x.TestSuiteName == SharedTestConstants.Suite1FullNameWithPreconditionTests);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                config.Excluding(x => x.TimeStamp)
                    .Excluding(x => x.ThreadId)
                    .Excluding(x => x.TestSuiteId));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.TestSuiteId.Should().MatchRegex(IdPattern);
            }
        }

        #endregion [OneTimeTearDown]

        #region [SuiteEnd]

        [Test]
        public void Should_Log_Contain_Suite1_SuiteEnd_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsSuite1.Single(x => x.TestSuiteName == SharedTestConstants.Suite1FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestSuiteName == SharedTestConstants.Suite1FullNameWithPreconditionTests && x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.SuiteEnd.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.EndTime)
                        .Excluding(x => x.Duration));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.EndTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.Duration.Should().MatchRegex(TimeSpanRegex);
            }
        }

        #endregion [SuiteEnd]
    }
}
