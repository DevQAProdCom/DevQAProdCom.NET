using ApplicationName.QA.Global.Models;
using DevQAProdCom.NET.DependencyInjection.Microsoft.OperativeClasses;
using DevQAProdCom.NET.Logging.TestRunners.Shared.Interfaces;

namespace ApplicationName.QA.TestsBasis.DependencyInjection
{
    internal class DiContainer : MicrosoftDiContainer
    {
        private static readonly Lazy<DiContainer> _instance = new Lazy<DiContainer>(() => new DiContainer());
        public static DiContainer Instance => _instance.Value;

        private AppSettings _appSettings;
        public AppSettings AppSettings
        {
            get
            {
                if (_appSettings == null)
                    _appSettings = GetRequiredService<AppSettings>();

                return _appSettings;
            }
        }

        private ITestLogger _log;
        public ITestLogger Log
        {
            get
            {
                if (_log == null)
                    _log = GetRequiredService<ITestLogger>();

                return _log;
            }
        }
    }
}
