using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    internal static partial class CommonsFunctions
    {
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
    }
}
