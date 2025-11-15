using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public class BaseUiInteractorFactory
    {
        protected virtual string DefaultBrowser => BrowserName.Chrome.ToString().ToLower();
        protected virtual string LocalBrowser => DefaultBrowser;
        public virtual string Browser => Environment.GetEnvironmentVariable(SharedUiConstants.BROWSER)?.ToLower() ?? LocalBrowser.ToLower();
        public virtual string BrowserVersion => Environment.GetEnvironmentVariable(SharedUiConstants.BROWSER_VERSION) ?? "latest";
        protected virtual string GetBaseDownloadsDefaultDirectory() => SharedUiConstants.DownloadsDefaultDirectory;
        protected virtual string GetDownloadsDefaultDirectory() => Path.Combine(GetBaseDownloadsDefaultDirectory(), $"{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}_{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}");
    }
}
