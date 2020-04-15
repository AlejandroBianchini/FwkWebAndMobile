using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Core
{
    public class AppSettingsConfiguration
    {
        private string driverPath;
        private string webDriverType;
        private string headdless;
        private string htmlReportsPath;

        public AppSettingsConfiguration()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var config = new ConfigurationBuilder()
            .AddJsonFile(path, false)
            .Build();

            var appSetting = config.GetSection("ConfigSection");
            driverPath = appSetting["DriverPath"];
            webDriverType = appSetting["WebDriverType"];
            headdless = appSetting["Headdless"];
            htmlReportsPath = appSetting["HtmlReportsPath"];
        }

        public string DriverPath
        {
            get => driverPath;
        }

        public string WebDriverType
        {
            get => webDriverType;
        }

        public string Headdless
        {
            get => headdless;
        }

        public string HtmlReportsPath
        {
            get => htmlReportsPath;
        }
    }
}
