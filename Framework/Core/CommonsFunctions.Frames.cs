using OpenQA.Selenium;
using System;

namespace Core
{
    internal static partial class CommonsFunctions
    {
        public static IWebDriver SwitchToFrame(String frameName)
        {
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
    }
}
