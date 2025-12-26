using DevQAProdCom.NET.UI.Selenium.WebDrivers.Interfaces;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;

namespace DevQAProdCom.NET.UI.Selenium.WebDrivers.OperativeClasses
{
    public class SeleniumUiInteractorConfiguration : BaseUiInteractorConfiguration, ISeleniumUiInteractorConfiguration
    {
        //Do not remove BaseDownloadsDefaultDirectory property. Is required, because Selenium needs absolute path.
        private string? _baseDownloadsDefaultDirectory;
        protected override string BaseDownloadsDefaultDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_baseDownloadsDefaultDirectory))
                {
                    _baseDownloadsDefaultDirectory = Path.Combine(Environment.CurrentDirectory, base.BaseDownloadsDefaultDirectory);
                }
                return _baseDownloadsDefaultDirectory;
            }
            set
            {
                _baseDownloadsDefaultDirectory = value;
            }
        }
    }
}
