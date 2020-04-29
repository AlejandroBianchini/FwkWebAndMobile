using NUnit.Framework;
using OpenQA.Selenium;
using PagesProject.MobilePages;
using PagesProject.WebPages;

namespace WebTestProject.ExampleTest
{
    [TestFixture]
    public class HomePageTest : BaseTest
    {
        TestContext testContext;        
        HomePage HomePage;
        IWebDriver driver;

        [SetUp]        
        public void Initialize()
        {
            testContext = TestContext.CurrentContext;
            reportHelper.StartTest(testContext);                       
        }

        [Test]               
        [TestCaseSource(typeof(BaseTest), "BrowserToRunWith")]    
        public void Test1(string browserName)
        {
            driver = GetWebDriver(browserName);
            HomePage = new HomePage(driver);

            HomePage.GoTo();
            HomePage.BuscarGoogle("Selenium");
            Assert.IsTrue(true);
        }

        [Test]        
        [TestCaseSource(typeof(BaseTest), "BrowserToRunWith")]
        public void TestWebAndMobile(string browserName)
        {
            driver = GetWebDriver(browserName);
            HomePage = new HomePage(driver);
            
            HomePage.GoTo();
            HomePage.BuscarGoogle("Segundo test");
            
            var _driver = GetMobileDriver(ANDROID_DRIVER);
            AndroidCalculator androidCalculator = new AndroidCalculator(_driver);
            androidCalculator.RealizarSuma();
            _driver.Quit();

            Assert.IsTrue(true);
        }

        [TearDown]
        public void CleanUp()
        {
            reportHelper.AddTestToReport(testContext);            
            driver.Quit();
            HomePage.KillDriverProcesses();
        }
    }
}
