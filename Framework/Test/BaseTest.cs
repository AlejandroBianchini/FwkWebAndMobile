using System;
using System.Collections.Generic;
using Core.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Test
{
    [SetUpFixture]
    [Parallelizable]
    public class BaseTest
    {
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

        public static IEnumerable<String> BrowserToRunWith()
        {
            String[] browser = { "Chrome", "Firefox", "IExplorer" };

            foreach (String b in browser)
            {
                yield return b;
            }
        }

        public IWebDriver Setup(String browser)
        {
            IWebDriver driver;
            if (browser == "Chrome")
            {
                driver = DriverHelper.FactoryDriver(browser);
            }
            else if (browser == "Firefox")
            {
                driver = DriverHelper.FactoryDriver(browser);
            }
            else
            {
                driver = DriverHelper.FactoryDriver(browser);
            }
            return driver;
        }
    }
}
