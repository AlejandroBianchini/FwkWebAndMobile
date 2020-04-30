using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;

namespace Core.Extension
{
    public static class WebElementMobileActions
    {
        private static readonly AppiumDriver<AppiumWebElement> mobileDriver = (AppiumDriver<AppiumWebElement>) CommonsFunctions.driver;

        public static void Tap(this IWebElement element)
        {
            TouchAction touchAction = new TouchAction(mobileDriver);
            touchAction.Tap(element).Perform();
        }

        public static void LongPress(this IWebElement element)
        {
            TouchAction touchAction = new TouchAction(mobileDriver);
            touchAction.LongPress(element).Perform();
        }

        public static void MoveTo(this IWebElement element)
        {
            TouchAction touchAction = new TouchAction(mobileDriver);
            touchAction.MoveTo(element).Perform();
        }
    }
}
