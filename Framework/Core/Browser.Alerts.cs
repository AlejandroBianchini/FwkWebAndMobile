using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    internal static partial class Browser
    {
        public static IAlert Alert
        {
            get { return Browser.SwitchTo().Alert(); }
        }

        public static void AlertAcept()
        {
            Browser.SwitchTo().Alert().Accept();
        }

        public static void AlertDismiss()
        {
            Browser.SwitchTo().Alert().Dismiss();
        }

        public static String AlertGetText()
        {
            return Browser.SwitchTo().Alert().Text;
        }
    }
}
