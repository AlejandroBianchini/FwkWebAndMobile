using System;
using System.Collections.Generic;
using Core.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Test
{
    [SetUpFixture]
    [Parallelizable(ParallelScope.Fixtures)]
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

        protected static IEnumerable<String> BrowserToRunWith()
        {
            String[] browser = { "Chrome", "Firefox"};

            foreach (String b in browser)
            {
                yield return b;
            }
        }

        protected IWebDriver Setup(String browser)
        {
            return DriverHelper.FactoryDriver(browser);
        }
    }
}
