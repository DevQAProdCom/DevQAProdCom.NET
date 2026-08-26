using System.Reflection;
using System.Text;
using DevQAProdCom.NET.Global.Extensions.StringExtensions;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Enumerations;
using FluentAssertions;

namespace Tests.DevQAProdCom.NET.Global.Tests
{
    public class Tests_IoStringExtensions
    {
        private static string InvokeToTruncatedFileNameOrDefault(string fileName, string? extension, int maxAmountOfChars, int budget, FileSystemNamingLimitUnit unit, int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = 7)
        {
            var method = typeof(IoStringExtensions).GetMethod("ToTruncatedFileNameOrDefault", BindingFlags.NonPublic | BindingFlags.Static)
                ?? throw new Exception("Private method not found");
            try
            {
                return (string)method.Invoke(null, new object?[] { fileName, extension, maxAmountOfChars, budget, unit, minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation })!;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException!;
            }
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WithShortNameAndExtension_ReturnsFullName()
        {
            var result = "report".ToTruncatedFileNameOrDefault("pdf");

            result.Should().Be("report.pdf");
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WithNullExtension_ReturnsFullNameWithoutExtension()
        {
            var result = "report".ToTruncatedFileNameOrDefault();

            result.Should().Be("report");
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WithExtensionStartingWithDot_ReturnsNormalizedExtension()
        {
            var result = "report".ToTruncatedFileNameOrDefault(".pdf");

            result.Should().Be("report.pdf");
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WithInvalidChars_ReplacesInvalidChars()
        {
            var result = "re:port".ToTruncatedFileNameOrDefault("pdf");

            result.Should().Be("re_port.pdf");
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WhenNameFitsWithinLimit_ReturnsFullName()
        {
            var result = "short".ToTruncatedFileNameOrDefault("txt", 10);

            result.Should().Be("short.txt");
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WhenNameExceedsLimit_TruncatesName()
        {
            var result = "thisisaverylongfilename".ToTruncatedFileNameOrDefault("txt", 10);

            result.Should().Be("thisisaxxx.txt");
            result.Length.Should().Be(14);
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WithLargeMaxAmountOfChars_ThrowsArgumentException()
        {
            Action act = () => "short".ToTruncatedFileNameOrDefault("txt", int.MaxValue);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WithZeroMaxAmountOfChars_ThrowsArgumentException()
        {
            Action act = () => "report".ToTruncatedFileNameOrDefault("txt", 0);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WithMaxAmountOfCharsAboveLimit_ThrowsArgumentException()
        {
            Action act = () => "report".ToTruncatedFileNameOrDefault("txt", 256);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_WithNullInput_ThrowsArgumentNullException()
        {
            string? input = null;

            Action act = () => input!.ToTruncatedFileNameOrDefault();

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_BytesPath_WhenNameFitsWithinLimit_ReturnsFullName()
        {
            var result = InvokeToTruncatedFileNameOrDefault("short", ".txt", 10, 255, FileSystemNamingLimitUnit.Bytes);

            result.Should().Be("short.txt");
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_BytesPath_WhenNameExceedsLimit_TruncatesName()
        {
            var result = InvokeToTruncatedFileNameOrDefault("thisisaverylongfilename", ".txt", 10, 255, FileSystemNamingLimitUnit.Bytes);

            result.Should().Be("thisisaxxx.txt");
            Encoding.UTF8.GetByteCount(result).Should().Be(14);
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_BytesPath_WithLargeMaxAmountOfChars_ThrowsArgumentException()
        {
            Action act = () => InvokeToTruncatedFileNameOrDefault("short", ".txt", int.MaxValue, 255, FileSystemNamingLimitUnit.Bytes);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_BytesPath_WithNonAsciiName_TruncatesByBytes()
        {
            var name = new string('あ', 100);

            var result = InvokeToTruncatedFileNameOrDefault(name, ".txt", 50, 255, FileSystemNamingLimitUnit.Bytes);

            Encoding.UTF8.GetByteCount(result).Should().BeLessOrEqualTo(255);
            result.Should().EndWith(".txt");
        }

        [Test]
        public void ToTruncatedFileNameOrDefault_BytesPath_WhenBudgetTooSmallForMinimum_ThrowsException()
        {
            Action act = () => InvokeToTruncatedFileNameOrDefault("short", ".txt", 7, 8, FileSystemNamingLimitUnit.Bytes);

            act.Should().Throw<Exception>();
        }

        [Test]
        public void ToFilePathWithFileNameTruncationOrDefault_WithValidInputs_ReturnsCombinedPath()
        {
            var result = "report".ToFilePathWithFileNameTruncationOrDefault("pdf", "C:\\temp");

            result.Should().Be("C:\\temp\\report.pdf");
        }

        [Test]
        public void ToFilePathWithFileNameTruncationOrDefault_WithEmptyDirectory_ThrowsArgumentException()
        {
            Action act = () => "report".ToFilePathWithFileNameTruncationOrDefault("pdf", "");

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ToFilePathWithFileNameTruncationOrDefault_WhenDirectoryPathTooLong_ThrowsException()
        {
            var longDirectory = "C:\\" + new string('a', 300);

            Action act = () => "report".ToFilePathWithFileNameTruncationOrDefault("pdf", longDirectory);

            act.Should().Throw<Exception>();
        }

        [Test]
        public void ToFilePathWithFileNameTruncationOrDefault_WithLargeMaxAmountOfChars_ThrowsArgumentException()
        {
            Action act = () => "report".ToFilePathWithFileNameTruncationOrDefault("pdf", "C:\\temp", int.MaxValue);

            act.Should().Throw<ArgumentException>();
        }
    }
}
