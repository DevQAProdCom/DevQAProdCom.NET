using System.Globalization;

namespace DevQAProdCom.NET.AI.Shared.Utils
{
    public static class SharedAiIoUtils
    {
        public static string GetTempAiInterationSessionFolder()
        {
            return Path.Combine(Path.GetTempPath(), "AiInterationSession" + DateTime.UtcNow.ToString("yyyy-MM-dd_hh-mm-ss.fffffff", CultureInfo.InvariantCulture));
        }
    }
}
