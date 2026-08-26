using System.Text;
using DevQAProdCom.NET.Global.Constants;
using DevQAProdCom.NET.Global.Utils;

namespace DevQAProdCom.NET.Global.Extensions.StringExtensions
{
    public static class IoStringExtensions
    {
        public static Stream ToStream(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                throw new ArgumentException(nameof(@string));

            byte[] byteArray = Encoding.UTF8.GetBytes(@string);
            return new MemoryStream(byteArray);
        }

        public static string ToTruncatedFileNameOrDefault(this string @string, string? extension = null,
            int? maxAmountOfCharsInFileName = null,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            return IoUtils.ToTruncatedFileNameOrDefault(@string, extension,
                maxAmountOfCharsInFileName: maxAmountOfCharsInFileName,
                minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation: minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);
        }

        public static string ToFilePathWithFileNameTruncationOrDefault(this string fileName, string extension, string directoryPath,
            int? maxAmountOfCharsInFileName = null,
            int minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation = GlobalConst.Io.MINIMUM_REQUIRED_CHARS_LENGTH_OF_FILE_NAME_WITHOUT_EXTENSION_TO_APPLY_TRUNCATION)
        {
            return IoUtils.ToFilePathWithFileNameTruncationOrDefault(fileName, extension, directoryPath,
                maxAmountOfCharsInFileName: maxAmountOfCharsInFileName,
                minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation: minimumRequiredCharsLengthOfFileNameWithoutExtensionToApplyTruncation);
        }
    }
}
