using System;
using System.IO;
using Core.MobileCapabilities;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Core.Helpers
{
    public static class DriverHelper
    {
        public static IWebDriver FactoryWebDriver(string browser)
        {
            AppSettingsConfiguration appSettings = new AppSettingsConfiguration();

            //String path = AppDomain.CurrentDomain.BaseDirectory + appSettings.WebDriverPath;
            string path = Path.Combine(Directory.GetCurrentDirectory(), appSettings.WebDriverPath);
            if (path.Contains("MobileTestProject"))
            {
                path = path.Replace("MobileTestProject", "WebTestProject");
            }
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

        public static AppiumDriver<AppiumWebElement> FactoryMobileDriver(string platformName)
        {
            AppSettingsConfiguration appSetings = new AppSettingsConfiguration();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "device.json");
            if (path.Contains("WebTestProject"))
            {
                path = path.Replace("WebTestProject", "MobileTestProject");
            }
            PlatformMobile platform = JsonConvert.DeserializeObject<PlatformMobile>(File.ReadAllText(path));
            AppiumOptions options = new AppiumOptions();
            string driverExecutablePath = Path.Combine(Directory.GetCurrentDirectory(), appSetings.MobileDriverPath + "/chromedriver.exe");

            switch (platformName)
            {
                case "Android":
                    options.AddAdditionalCapability(MobileCapabilityType.DeviceName, platform.Android.DeviceName);
                    options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, platform.Android.PlatformVersion);
                    options.AddAdditionalCapability(MobileCapabilityType.PlatformName, platform.Android.PlatformName);
                    options.AddAdditionalCapability(MobileCapabilityType.BrowserName, platform.Android.BrowserName);
                    options.AddAdditionalCapability(MobileCapabilityType.Udid, platform.Android.Udid);
                    options.AddAdditionalCapability(MobileCapabilityType.Language, platform.Android.Language);
                    options.AddAdditionalCapability(MobileCapabilityType.Orientation, platform.Android.Orientation);
                    options.AddAdditionalCapability(MobileCapabilityType.NoReset, platform.Android.NoReset);
                    options.AddAdditionalCapability(MobileCapabilityType.FullReset, platform.Android.FullReset);
                    options.AddAdditionalCapability(MobileCapabilityType.App, platform.Android.App);
                    options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, platform.Android.AppPackage);
                    options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, platform.Android.AppActivity);
                    options.AddAdditionalCapability(AndroidMobileCapabilityType.AppWaitActivity, platform.Android.AppWaitActivity);
                    options.AddAdditionalCapability(AndroidMobileCapabilityType.AppWaitPackage, platform.Android.AppWaitPackage);
                    options.AddAdditionalCapability(AndroidMobileCapabilityType.ChromedriverExecutable, driverExecutablePath);

                    return new AndroidDriver<AppiumWebElement>(new Uri(appSetings.UrlHub), options);
                case "iOS":
                    options.AddAdditionalCapability(MobileCapabilityType.DeviceName, platform.iOS.DeviceName);
                    options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, platform.iOS.PlatformVersion);
                    options.AddAdditionalCapability(MobileCapabilityType.PlatformName, platform.iOS.PlatformName);
                    options.AddAdditionalCapability(MobileCapabilityType.BrowserName, platform.iOS.BrowserName);
                    options.AddAdditionalCapability(MobileCapabilityType.Udid, platform.iOS.Udid);
                    options.AddAdditionalCapability(MobileCapabilityType.Language, platform.iOS.Language);
                    options.AddAdditionalCapability(MobileCapabilityType.Orientation, platform.iOS.Orientation);
                    options.AddAdditionalCapability(MobileCapabilityType.NoReset, platform.iOS.NoReset);
                    options.AddAdditionalCapability(MobileCapabilityType.FullReset, platform.iOS.FullReset);
                    options.AddAdditionalCapability(MobileCapabilityType.App, platform.iOS.App);
                    options.AddAdditionalCapability(IOSMobileCapabilityType.AppName, platform.iOS.AppName);

                    return new IOSDriver<AppiumWebElement>(new Uri(appSetings.UrlHub), options);
                default:
                    throw new Exception("La plataforma indicada no existe.");
            }
        }
    }
}
