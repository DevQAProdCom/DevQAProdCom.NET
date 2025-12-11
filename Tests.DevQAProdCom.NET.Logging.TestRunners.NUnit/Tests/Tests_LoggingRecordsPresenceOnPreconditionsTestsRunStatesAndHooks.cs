using DevQAProdCom.NET.Logging.TestRunners.Shared.Enumerations;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Hooks;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.TestData;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Tests
{
    [Order(1)]
    internal class LoggingRecordsPresenceTestsOnPreconditionsTestsRunLevel : BaseTest
    {
        #region [RunStart]

        [Test]
        public void Should_Log_Contain_RunStart_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsRunStatesAndHooks.Single(x => x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.RunStart.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.RunStart.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.LogEntryOrderNumber)
                        .Excluding(x => x.StartTime)
                        .Excluding(x => x.AdditionalInfo));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
                actualFullLogRecord?.StartTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.AdditionalInfo.Should().ContainKey("CommandLine");
            }
        }

        #endregion [RunStart]

        #region [RunOneTimeSetUpExecution]

        [Test]
        public void Should_Log_Contain_RunHooks_OneTimeSetUp_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsRunStatesAndHooks.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.a18dee14);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.a18dee14);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.HookClassId)
                        .Excluding(x => x.LogEntryOrderNumber));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.HookClassId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
            }
        }

        #endregion [RunOneTimeSetUpExecution]

        #region [RunOneTimeTearDownExecution]

        [Test]
        public void Should_Log_Contain_RunHooks_OneTimeTearDown_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsRunStatesAndHooks.Single(x => x.LogRecordId == SharedTestConstants.LogRecordId.ab95bec6);

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.LogRecordId == SharedTestConstants.LogRecordId.ab95bec6);

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.HookClassId)
                        .Excluding(x => x.LogEntryOrderNumber));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.HookClassId.Should().MatchRegex(IdPattern);
                actualFullLogRecord?.LogEntryOrderNumber.Should().ContainAny("0", "1", "2");
            }
        }

        #endregion [RunOneTimeTearDownExecution]

        #region [RunEnd]

        [Test]
        public void Should_Log_Contain_RunEnd_Data()
        {
            //GIVEN
            var expectedLogRecord = ExpectedLogRecords.PreconditionsTestsRunStatesAndHooks.Single(x => x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.RunEnd.ToString());

            //WHEN
            var actualFullLogRecord = ActualFullLogModels.SingleOrDefault(x => x.TestRunExecutionFlowStage == TestRunExecutionFlowStage.RunEnd.ToString());

            //THEN
            using (new AssertionScope())
            {
                actualFullLogRecord.Should().BeEquivalentTo(expectedLogRecord, config: config =>
                    config.Excluding(x => x.TimeStamp)
                        .Excluding(x => x.ThreadId)
                        .Excluding(x => x.LogEntryOrderNumber)
                        .Excluding(x => x.EndTime)
                        .Excluding(x => x.Duration));

                actualFullLogRecord?.TimeStamp.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.ThreadId.Should().HaveValue();
                actualFullLogRecord?.LogEntryOrderNumber.Should().Be("3");
                actualFullLogRecord?.EndTime.Should().MatchRegex(DateTimeRegex);
                actualFullLogRecord?.Duration.Should().MatchRegex(TimeSpanRegex);
            }
        }

        #endregion [RunEnd]
    }
}
