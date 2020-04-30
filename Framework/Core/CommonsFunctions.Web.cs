using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Core
{
    internal static partial class CommonsFunctions
    {
        public static void GoTo(string url)
        {
            driver.Url = url;
        }

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

        #region Windows

        /// <summary>
        /// Cambia el foco a la primer Window cuya URL contenga el parámetro url recibido.
        /// </summary>
        public static void SwitchToWindowByUrl(String url)
        {
            var windows = CommonsFunctions.GetAllWindows();
            foreach (var window in windows)
            {
                Boolean esVentanaSolicitada = CommonsFunctions.SwitchTo().Window(window).Url.Contains(url);
                if (esVentanaSolicitada)
                {
                    CommonsFunctions.SwitchTo().Window(window);
                    break;
                }
            }
        }

        public static void SwitchToDefaultContent()
        {
            CommonsFunctions.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Cambia el foco a la primer Window cuyo TITLE contenga el parámetro title recibido.
        /// </summary>
        public static void SwitchToWindowByTitle(String title)
        {
            var windows = CommonsFunctions.GetAllWindows();
            foreach (var window in windows)
            {
                Boolean esVentanaSolicitada = CommonsFunctions.SwitchTo().Window(window).Title.Contains(title);
                if (esVentanaSolicitada)
                {
                    CommonsFunctions.SwitchTo().Window(window);
                    break;
                }
            }
        }

        public static List<String> GetAllWindows()
        {
            return driver.WindowHandles.ToList();
        }

        public static String CurrentWindow()
        {
            return driver.CurrentWindowHandle;
        }

        /// <summary>
        /// Encapsulamos funcionalidad en clase privada para consumir desde Frames, Alerts o Windows.-
        /// </summary>
        private static ITargetLocator SwitchTo()
        {
            return driver.SwitchTo();
        }

        #endregion

        #region Frames
        public static IWebDriver SwitchToFrame(String frameName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameName));
            return CommonsFunctions.SwitchTo().Frame(frameName);
        }

        public static IWebDriver SwitchToFrameElement(IWebElement frameElement)
        {
            return CommonsFunctions.SwitchTo().Frame(frameElement);
        }

        public static IWebDriver SwitchToDefaultFrame()
        {
            return CommonsFunctions.SwitchTo().DefaultContent();
        }

        #endregion

        #region Alerts
        public static IAlert Alert
        {
            get { return CommonsFunctions.SwitchTo().Alert(); }
        }

        public static void AlertAcept()
        {
            CommonsFunctions.SwitchTo().Alert().Accept();
        }

        public static void AlertDismiss()
        {
            CommonsFunctions.SwitchTo().Alert().Dismiss();
        }

        public static String AlertGetText()
        {
            return CommonsFunctions.SwitchTo().Alert().Text;
        }
        #endregion
    }
}
