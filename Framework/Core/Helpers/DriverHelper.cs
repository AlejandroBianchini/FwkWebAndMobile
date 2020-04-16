using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

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
                    return new InternetExplorerDriver(path);
                case "Chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("start-maximized");
                    if (headdless)
                    {
                        options.AddArgument("headless");
                        options.AddArgument("window-size=1920,1080");
                    }
                    return new ChromeDriver(path, options);
                default:
                    throw new Exception("El driver no existe. Ingrese uno de los siguientes: FireFox, IExplorer, Chrome");
            }
        }
    }
}
