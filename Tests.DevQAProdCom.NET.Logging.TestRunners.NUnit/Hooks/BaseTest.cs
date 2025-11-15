using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Constants;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Models;
using Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.PreconditionsTests.Constants;

namespace Tests.DevQAProdCom.NET.Logging.TestRunners.NUnit.Hooks
{
    internal class BaseTest
    {
        protected List<LogModel> ActualFullLogModels;

        protected string DateTimeRegex = @"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{7}Z$";
        protected string TimeSpanRegex = @"^\d{2}:\d{2}:\d{2}\.\d{7}$";
        protected string IdPattern = @"^\d+-\d+$";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var actualLogFiles = Directory.GetFiles(Const.DirectoryWithLogs);
            var actualTxtLogFilePath = actualLogFiles.SingleOrDefault(x => x.EndsWith(".txt"));
            var actualJsonLogFilePath = actualLogFiles.SingleOrDefault(x => x.EndsWith(".json"));

            using (new AssertionScope())
            {
                actualLogFiles.Length.Should().Be(2);

                //Check presence of generated JSON Log file
                bool isActualJsonLogFileExist = File.Exists(actualJsonLogFilePath);
                isActualJsonLogFileExist.Should().BeTrue($"JSON log file '{Path.Combine(Environment.CurrentDirectory, Const.DirectoryWithLogs, $"{SharedTestConstants.LogPrefix}{SharedTestConstants.ManuallySetTestRunId}_{{DateTime.UtcNow}}.json")}' should have been created");

                if (isActualJsonLogFileExist)
                    new FileInfo(actualJsonLogFilePath!).Name.Should().MatchRegex(Const.ExpectedJsonFileRegex);

                //Check presence of generated TXT Log file
                bool isActualTxtLogFileExist = File.Exists(actualTxtLogFilePath);
                isActualTxtLogFileExist.Should().BeTrue($"TXT log file '{Path.Combine(Environment.CurrentDirectory, Const.DirectoryWithLogs, $"{SharedTestConstants.LogPrefix}{SharedTestConstants.ManuallySetTestRunId}_{{DateTime.UtcNow}}.txt")}' should have been created");

                if (isActualTxtLogFileExist)
                    new FileInfo(actualTxtLogFilePath!).Name.Should().MatchRegex(Const.ExpectedTxtFileRegex);
            }

            ActualFullLogModels = File.ReadAllLines(actualJsonLogFilePath!).Select(x => JsonSerializer.Deserialize<LogModel>(x)).ToList();
        }
    }
}
