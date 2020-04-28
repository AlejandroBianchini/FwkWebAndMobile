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
        public static IWebDriver FactoryDriver(string browser)
        {
            AppSettingsConfiguration appSettings = new AppSettingsConfiguration();

            String path = AppDomain.CurrentDomain.BaseDirectory + appSettings.DriverPath;
            //String type = appSettings.WebDriverType;
            Boolean headdless = Convert.ToBoolean(appSettings.Headdless);

            switch (browser)
            {                
                case "Firefox":
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    if (Convert.ToBoolean(appSettings.RemoteDriver))
                    {
                        return new RemoteWebDriver(new Uri(appSettings.RemoteUrl), firefoxOptions);
                    }
                    else
                    {
                        return new FirefoxDriver(path, firefoxOptions);
                    }
                case "IExplorer":
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.IgnoreZoomLevel = true;
                    ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOptions.EnsureCleanSession = true;
                    if (Convert.ToBoolean(appSettings.RemoteDriver))
                    {
                        return new RemoteWebDriver(new Uri(appSettings.RemoteUrl), ieOptions);
                    }
                    else
                    {
                        return new InternetExplorerDriver(path);
                    }
                case "Chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("start-maximized");

                    if (headdless)
                    {
                        chromeOptions.AddArgument("headless");
                        chromeOptions.AddArgument("window-size=1280,720");
                    }

                    if (Convert.ToBoolean(appSettings.RemoteDriver))
                    {
                        return new RemoteWebDriver(new Uri(appSettings.RemoteUrl), chromeOptions);
                    }
                    else
                    {
                        return new ChromeDriver(path, chromeOptions);
                    }
                default:
                    throw new Exception("El driver no existe. Ingrese uno de los siguientes: FireFox, IExplorer, Chrome");
            }
        }
    }
}
