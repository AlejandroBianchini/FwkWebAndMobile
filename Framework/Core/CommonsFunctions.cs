using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Core.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
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

        public static void PageInit(IWebDriver driver, object page)
        {
            CommonsFunctions.driver = driver;
            PageFactory.InitElements(CommonsFunctions.driver, page);
        }

        //private static Boolean IsMobile(this IWebDriver driver)
        //{
        //    if (driver.GetType().Name.Contains("Android") || driver.GetType().Name.Contains("iOS"))
        //        return true;
        //    else
        //        return false;
        //}

        public static void GoTo(string url)
        {
            driver.Url = url;
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

        public static IWebElement ExplicitWait(Int32 time, Func<IWebDriver, IWebElement> explicitWaitFunc)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            return wait.Until(explicitWaitFunc);
        }

        public static bool ExplicitWait(Int32 time, Func<IWebDriver, bool> explicitWaitFunc)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            return wait.Until(explicitWaitFunc);
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
                    driver.Navigate().Back();
                    break;
                case Navigation.NavigationActions.GoFoward:
                    driver.Navigate().Forward();
                    break;
                case Navigation.NavigationActions.Refresh:
                    driver.Navigate().Refresh();
                    break;
                default:
                    break;
            }
        }

        public static void MoveToElement(IWebElement element)
        {
            Actions select = new Actions(driver);
            select.MoveToElement(element).Click();
            select.Perform();
        }

        /// <summary>
        /// Close de current Window.
        /// </summary>
        public static void CloseWindows()
        {
            driver.Close();
        }

        public static void DefaultContent()
        {
            driver.SwitchTo().DefaultContent();
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
            return driver.PageSource;
        }

        #region ExplicitWait

        public static void ExplicitWait(int time, ExpectedConditionsEnum expected, By by, string text = "")
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            switch (expected)
            {
                case ExpectedConditionsEnum.ElementExists:
                    wait.Until(ExpectedConditions.ElementExists(by));
                    break;
                case ExpectedConditionsEnum.ElementIsVisible:
                    wait.Until(ExpectedConditions.ElementIsVisible(by));
                    break;
                case ExpectedConditionsEnum.ElementToBeClickable:
                    wait.Until(ExpectedConditions.ElementToBeClickable(by));
                    break;
                case ExpectedConditionsEnum.ElementToBeSelected:
                    wait.Until(ExpectedConditions.ElementToBeSelected(by));
                    break;
                case ExpectedConditionsEnum.InvisibilityOfElementLocated:
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
                    break;
                case ExpectedConditionsEnum.InvisibilityOfElementWithText:
                    wait.Until(ExpectedConditions.InvisibilityOfElementWithText(by, text));
                    break;
                case ExpectedConditionsEnum.PresenceOfAllElementsLocatedBy:
                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
                    break;
                case ExpectedConditionsEnum.TextToBePresentInElementLocated:
                    wait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, text));
                    break;
                case ExpectedConditionsEnum.TextToBePresentInElementValue:
                    wait.Until(ExpectedConditions.TextToBePresentInElementValue(by, text));
                    break;
                case ExpectedConditionsEnum.VisibilityOfAllElementsLocatedBy:
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                    break;
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        public static void ExplicitWait(int time, ExpectedConditionsEnum expected, IWebElement element, string text = "")
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            switch (expected)
            {
                case ExpectedConditionsEnum.ElementToBeClickable:
                    wait.Until(ExpectedConditions.ElementToBeClickable(element));
                    break;
                case ExpectedConditionsEnum.ElementToBeSelected:
                    wait.Until(ExpectedConditions.ElementToBeSelected(element));
                    break;
                case ExpectedConditionsEnum.StalenessOf:
                    wait.Until(ExpectedConditions.StalenessOf(element));
                    break;
                case ExpectedConditionsEnum.TextToBePresentInElement:
                    wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));
                    break;
                case ExpectedConditionsEnum.TextToBePresentInElementValue:
                    wait.Until(ExpectedConditions.TextToBePresentInElementValue(element, text));
                    break;
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        public static void ExplicitWait(int time, ExpectedConditionsEnum expected, string text)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            switch (expected)
            {
                case ExpectedConditionsEnum.FrameToBeAvailableAndSwitchToIt:
                    wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(text));
                    break;
                case ExpectedConditionsEnum.TitleContains:
                    wait.Until(ExpectedConditions.TitleContains(text));
                    break;
                case ExpectedConditionsEnum.TitleIs:
                    wait.Until(ExpectedConditions.TitleIs(text));
                    break;
                case ExpectedConditionsEnum.UrlContains:
                    wait.Until(ExpectedConditions.UrlContains(text));
                    break;
                case ExpectedConditionsEnum.UrlMatches:
                    wait.Until(ExpectedConditions.UrlMatches(text));
                    break;
                case ExpectedConditionsEnum.UrlToBe:
                    wait.Until(ExpectedConditions.UrlToBe(text));
                    break;
                default:
                    throw new Exception("Condition Doesn't exist");
            }
        }

        #endregion

    }

}
