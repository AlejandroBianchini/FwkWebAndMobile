using NUnit.Framework;
using OpenQA.Selenium;
using ProjectToTest.pages;

namespace Test.ExamplePageTest
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
            driver = Setup(browserName);
            HomePage = new HomePage(driver);

            HomePage.GoTo();
            HomePage.BuscarGoogle("Selenium");
            Assert.IsTrue(true);
        }

        [Test]        
        [TestCaseSource(typeof(BaseTest), "BrowserToRunWith")]
        public void TestReport2(string browserName)
        {
            driver = Setup(browserName);
            HomePage = new HomePage(driver);

            HomePage.GoTo();
            HomePage.BuscarGoogle("Segundo test");
            Assert.IsTrue(true);
        }

        [TearDown]
        public void CleanUp()
        {
            reportHelper.AddTestToReport(testContext);
            driver.Quit();
        }
    }
}
