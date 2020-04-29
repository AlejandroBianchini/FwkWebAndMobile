using System;
using System.Collections.Generic;
using Core.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace WebTestProject
{
    [SetUpFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class BaseTest
    {
        protected string ANDROID_DRIVER = "Android";
        protected string IOS_DRIVER = "iOS";

        public ReportHelper reportHelper = new ReportHelper();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            reportHelper.GenerateReport();            
        }

        protected static IEnumerable<String> BrowserToRunWith()
        {
            String[] browser = { "Chrome", "Firefox"};

            foreach (String b in browser)
            {
                yield return b;
            }
        }

        protected IWebDriver GetWebDriver(string browser)
        {
            return DriverHelper.FactoryWebDriver(browser);
        }

        protected AppiumDriver<AppiumWebElement> GetMobileDriver(string browser)
        {
            return DriverHelper.FactoryMobileDriver(browser);
        }
    }
}
