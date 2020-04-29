using Core.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace MobileTestProject.AndroidPages
{
    [SetUpFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AndroidBaseTest
    {
        protected ReportHelper report = new ReportHelper();
        private const string ANDROID_DRIVER = "Android";
        protected const string CHROME_DRIVER = "Chrome";
        protected AppiumDriver<AppiumWebElement> driver;

        public AppiumDriver<AppiumWebElement> GetDriver()
        {
            return DriverHelper.FactoryMobileDriver(ANDROID_DRIVER);
        }

        public IWebDriver GetWebDriver(string browser)
        {
            return DriverHelper.FactoryWebDriver(CHROME_DRIVER);
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
