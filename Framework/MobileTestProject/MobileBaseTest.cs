using Core.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace MobileTestProject.AndroidPages
{
    [SetUpFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class MobileBaseTest
    {
        protected ReportHelper report = new ReportHelper();
        protected AppiumDriver<AppiumWebElement> driver;

        public AppiumDriver<AppiumWebElement> GetDriver(string type)
        {
            return DriverHelper.FactoryMobileDriver(type);
        }

        public IWebDriver GetWebDriver(string browser)
        {
            return DriverHelper.FactoryWebDriver(browser);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

    }
}
