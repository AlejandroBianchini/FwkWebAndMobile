using Core;
using NUnit.Framework;
using PagesProject.MobilePages;
using PagesProject.WebPages;

namespace MobileTestProject.AndroidPages
{
    [TestFixture]
    public class AndroidCalculatorTest : MobileBaseTest
    {
        public TestContext testContext;
        AndroidCalculator androidCalculator;

        [SetUp]
        public void Setup()
        {
            testContext = TestContext.CurrentContext;
            report.StartTest(testContext);
            driver = GetDriver(DriverType.Android);
            androidCalculator = new AndroidCalculator(driver);
        }

        [Test]
        public void Test1()
        {
            androidCalculator.RealizarSuma();

            Assert.AreEqual("11", androidCalculator.ValidarCuenta());
        }

        [Test]
        public void TestMobileAndWeb()
        {
            androidCalculator.RealizarSuma();

            var _driver = GetWebDriver(DriverType.Chrome);
            HomePage homePage = new HomePage(_driver);

            homePage.GoTo();
            homePage.BuscarGoogle("Selenium");
            _driver.Quit();

            Assert.AreEqual("11", androidCalculator.ValidarCuenta());
        }

        [TearDown]
        public void TearDown()
        {
            report.AddTestToReport(testContext);
            driver.Quit();
        }
    }
}
