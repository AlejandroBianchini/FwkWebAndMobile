using Core.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Core
{
    internal static partial class Browser
    {
        private const String INTERNET_EXPLORER_DRIVER = "internetexplorerdriver";
        private const String CHROME_DRIVER = "chromedriver";
        private const String FIREFOX_DRIVER = "geckodriver.exe";
        private const String INTERNET_EXPLORER_DRIVER_SERVER = "iedriverserver";
        public static IWebDriver webDriver;

        public static ISearchContext Driver
        {
            get
            {
                return webDriver;
            }
        }

        public static void PageInit(IWebDriver driver, object page)
        {
            webDriver = driver;
            PageFactory.InitElements(Driver, page);
        }

        public static void GoTo(string url)
        {
            webDriver.Url = url;
        }

        public static void PrintScreen(string fileName, ScreenshotImageFormat imageFormat, string path = null)
        {
            if (String.IsNullOrEmpty(path))
                path = ConfigurationManager.AppSettings["DefaultImagePath"];
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
           
            var file = path + fileName + "." + imageFormat.ToString();
            Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
            ss.SaveAsFile(file);
        }

        public static void Quit()
        {
            webDriver.Quit();
        }

        public static void KillDriverProcesses()
        {
            var driverName = webDriver.GetType().Name.ToLower();
            string processName = String.Empty;
            switch (driverName)
            {
                case INTERNET_EXPLORER_DRIVER:
                    processName = INTERNET_EXPLORER_DRIVER_SERVER;
                    break;
                case CHROME_DRIVER:
                    processName = CHROME_DRIVER;
                    break;
                case FIREFOX_DRIVER:
                    processName = FIREFOX_DRIVER;
                    break;
            }
            if (!String.IsNullOrEmpty(processName))
            {
                var processes = Process.GetProcesses().Where(p => p.ProcessName.ToLower() == processName);
                foreach (var process in processes)
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                }
            }
        }

        public static IWebElement ExplicitWait(Int32 time, Func<IWebDriver, IWebElement> explicitWaitFunc)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(time));
            return wait.Until(explicitWaitFunc);
        }

        public static bool ExplicitWait(Int32 time, Func<IWebDriver, bool> explicitWaitFunc)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(time));
            return wait.Until(explicitWaitFunc);
        }

        public static bool FindElementIfExists(By by)
        {
            try
            {
                webDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //public static void Initializes(bool maximized = true)
        //{
        //    try
        //    {
        //        if (maximized)
        //            Browser.webDriver.Manage().Window.Maximize();
        //    }
        //    catch
        //    {
        //        webDriver = DriverHelper.FactoryDriver();
        //        Initializes();
        //    }
        //}

        public static void Action(Navigation.NavigationActions action)
        {
            switch (action)
            {
                case Navigation.NavigationActions.GoBack:
                    webDriver.Navigate().Back();
                    break;
                case Navigation.NavigationActions.GoFoward:
                    webDriver.Navigate().Forward();
                    break;
                case Navigation.NavigationActions.Refresh:
                    webDriver.Navigate().Refresh();
                    break;
                default:
                    break;
            }
        }

        public static void MoveToElement(IWebElement element)
        {
            Actions select = new Actions(webDriver);
            select.MoveToElement(element).Click();
            select.Perform();
        }

        /// <summary>
        /// Close de current Window.
        /// </summary>
        public static void CloseWindows()
        {
            webDriver.Close();
        }

        public static void DefaultContent()
        {
            webDriver.SwitchTo().DefaultContent();
        }

        public static Boolean Javas(String jav)
        {
            ((IJavaScriptExecutor)webDriver).ExecuteScript(jav);
            return true;
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By obj)
        {
            ReadOnlyCollection<IWebElement> elements = webDriver.FindElements(obj);

            return elements;
        }

        public static IWebElement FindNestedElements(By objeto1, By objeto2)
        {
            IWebElement elemento = webDriver.FindElement(objeto1).FindElement(objeto2);
            return elemento;
        }

        /// <summary>
        /// Scroll in the current screen. Must be indicate the value in porcents. Positives values make scroll down and negatives values make scroll up (optional).
        /// For default will make are scroll down of 50%.
        /// </summary>
        /// <param name="i"></param>
        public static void Scroll(string i = null)
        {
            if (String.IsNullOrEmpty(i))
            {
                i = "450";
            }
            else
            {
                i = Convert.ToString(10000 / Convert.ToInt32(i)); 
            }
            Javas($"window.scrollBy(0,{i})");
        }

        public static string PageSource()
        {
            return webDriver.PageSource;
        }
    }

}
