using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor
{
    public class BaseUiInteractorConfiguration : IUiInteractorConfiguration
    {
        //Do not remove GetBaseDownloadsDefaultDirectory method. Is required, because Selenium and Playwright have differences with setting relative and absolute paths.
        protected virtual string BaseDownloadsDefaultDirectory { get; set; } = SharedUiConstants.Configurations.DefaultDownloadsDirectory;

        private string? _downloadsDefaultDirectory;
        public virtual string DownloadsDefaultDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_downloadsDefaultDirectory))
                    _downloadsDefaultDirectory = Path.Combine(BaseDownloadsDefaultDirectory, $"{DateTime.UtcNow.ToFileNameSupportedFormatWithMicroseconds()}_{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}");

                return _downloadsDefaultDirectory;
            }
            set
            {
                _downloadsDefaultDirectory = value;
            }
        }

        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public TimeSpan? TimeToLive { get; set; } = SharedUiConstants.Configurations.DefaultUiInteractorTimeToLive;
        public DateTime? ExpirationTime => Created.HasValue && TimeToLive.HasValue ? Created.Value.Add(TimeToLive.Value) : null;
        public Dictionary<string, object>? Data { get; set; }
        public int UiElementsSearchImplicitWaitSeconds { get; set; } = SharedUiConstants.Configurations.DefaultUiElementsSearchImplicitWaitSeconds;
    }
}
