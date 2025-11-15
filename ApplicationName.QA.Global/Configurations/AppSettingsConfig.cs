using ApplicationName.QA.Global.Models;
using DevQAProdCom.NET.Configurations.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ApplicationName.QA.Global.Configurations
{
    public class AppSettingsConfig(IConfigContainer configContainer) : IConfig<AppSettings>
    {
        private AppSettings _appSettings;

        public AppSettings Get()
        {
            if (_appSettings == null)
            {
                configContainer.Config(x => x.AddJsonFile(Path.Combine("ConfigFiles", "appsettings.qa.json")));
                _appSettings = configContainer.Get<AppSettings>("appSettings");
            }

            return _appSettings;
        }
    }
}
