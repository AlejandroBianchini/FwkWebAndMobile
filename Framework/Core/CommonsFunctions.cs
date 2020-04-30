using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Core.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Core
{
    internal static partial class CommonsFunctions
    {
        private const String INTERNET_EXPLORER_DRIVER = "internetexplorerdriver";
        private const String CHROME_DRIVER = "chromedriver";
        private const String FIREFOX_DRIVER = "geckodriver.exe";
        private const String INTERNET_EXPLORER_DRIVER_SERVER = "iedriverserver";
        public static IWebDriver driver;

        public static ISearchContext WebDriver
        {
            get
            {
                return driver;
            }
        }   

        public static void PageInit(IWebDriver _driver, object page)
        {
            driver = _driver;
            PageFactory.InitElements(CommonsFunctions.driver, page);
        }    

        public static void PrintScreen(string fileName, ScreenshotImageFormat imageFormat, string path = null)
        {
            if (String.IsNullOrEmpty(path))
                path = ConfigurationManager.AppSettings["DefaultImagePath"];
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
           
            var file = path + fileName + "." + imageFormat.ToString();
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(file);
        }

        public static void Quit()
        {
            driver.Quit();
        }

        public static void KillDriverProcesses()
        {
            var driverName = driver.GetType().Name.ToLower();
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

        public static Boolean Javas(String jav)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(jav);
            return true;
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By obj)
        {
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(obj);

            return elements;
        }

        public static IWebElement FindNestedElements(By objeto1, By objeto2)
        {
            IWebElement elemento = driver.FindElement(objeto1).FindElement(objeto2);
            return elemento;
        }

        public static bool FindElementIfExists(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }        

        public static string PageSource()
        {
            return driver.PageSource;
        }

        #region ExplicitWait

        public static IWebElement ExplicitWait(int time, ExpectedConditionsEnum expected, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            switch (expected)
            {
                case ExpectedConditionsEnum.ElementExists:
                    return wait.Until(ExpectedConditions.ElementExists(by));                    
                case ExpectedConditionsEnum.ElementIsVisible:
                    return wait.Until(ExpectedConditions.ElementIsVisible(by));                    
                case ExpectedConditionsEnum.ElementToBeClickable:
                    return wait.Until(ExpectedConditions.ElementToBeClickable(by));
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        public static ReadOnlyCollection<IWebElement> ExplicitWait(ExpectedConditionsEnum expected, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            switch (expected)
            {
                case ExpectedConditionsEnum.PresenceOfAllElementsLocatedBy:
                    return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
                case ExpectedConditionsEnum.VisibilityOfAllElementsLocatedBy:
                    return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        public static Boolean ExplicitWait(int time, ExpectedConditionsEnum expected, By by, string text = "")
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            switch (expected)
            {
                case ExpectedConditionsEnum.ElementToBeSelected:
                    return wait.Until(ExpectedConditions.ElementToBeSelected(by));
                case ExpectedConditionsEnum.InvisibilityOfElementLocated:
                    return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
                case ExpectedConditionsEnum.InvisibilityOfElementWithText:
                    return wait.Until(ExpectedConditions.InvisibilityOfElementWithText(by, text));
                case ExpectedConditionsEnum.TextToBePresentInElementLocated:
                    return wait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, text));
                case ExpectedConditionsEnum.TextToBePresentInElementValue:
                    return wait.Until(ExpectedConditions.TextToBePresentInElementValue(by, text));
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        public static IWebElement ExplicitWait(int time, ExpectedConditionsEnum expected, IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            switch (expected)
            {
                case ExpectedConditionsEnum.ElementToBeClickable:
                    return wait.Until(ExpectedConditions.ElementToBeClickable(element));                                    
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        public static Boolean ExplicitWait(int time, ExpectedConditionsEnum expected, IWebElement element, string text = "")
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            switch (expected)
            {
                case ExpectedConditionsEnum.ElementToBeSelected:
                    return wait.Until(ExpectedConditions.ElementToBeSelected(element));
                case ExpectedConditionsEnum.StalenessOf:
                    return wait.Until(ExpectedConditions.StalenessOf(element));
                case ExpectedConditionsEnum.TextToBePresentInElement:
                    return wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));
                case ExpectedConditionsEnum.TextToBePresentInElementValue:
                    return wait.Until(ExpectedConditions.TextToBePresentInElementValue(element, text));
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        public static Boolean ExplicitWait(int time, ExpectedConditionsEnum expected, string text)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            switch (expected)
            {
                case ExpectedConditionsEnum.TitleContains:
                    return wait.Until(ExpectedConditions.TitleContains(text));
                case ExpectedConditionsEnum.TitleIs:
                    return wait.Until(ExpectedConditions.TitleIs(text));
                case ExpectedConditionsEnum.UrlContains:
                    return wait.Until(ExpectedConditions.UrlContains(text));
                case ExpectedConditionsEnum.UrlMatches:
                    return wait.Until(ExpectedConditions.UrlMatches(text));
                case ExpectedConditionsEnum.UrlToBe:
                    return wait.Until(ExpectedConditions.UrlToBe(text));
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        #endregion

    }

}
