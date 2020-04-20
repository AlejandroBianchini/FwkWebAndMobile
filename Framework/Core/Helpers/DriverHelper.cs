using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Core.Helpers
{
    public static class DriverHelper
    {
        public static IWebDriver FactoryDriver()
        {
            AppSettingsConfiguration appSettings = new AppSettingsConfiguration();

            String path = AppDomain.CurrentDomain.BaseDirectory + appSettings.DriverPath;
            String type = appSettings.WebDriverType;
            Boolean headdless = Convert.ToBoolean(appSettings.Headdless);

            switch (type)
            {                
                case "FireFox":
                    return new FirefoxDriver();
                case "IExplorer":
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.IgnoreZoomLevel = true;
                    ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOptions.EnsureCleanSession = true;
                    return new InternetExplorerDriver(path);
                case "Chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("start-maximized");
                    if (headdless)
                    {
                        chromeOptions.AddArgument("headless");
                        chromeOptions.AddArgument("window-size=1280,720");
                    }
                    return new ChromeDriver(path, chromeOptions);
                default:
                    throw new Exception("El driver no existe. Ingrese uno de los siguientes: FireFox, IExplorer, Chrome");
            }
        }
    }
}
