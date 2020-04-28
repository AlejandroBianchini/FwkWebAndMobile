using NUnit.Framework;
using OpenQA.Selenium;
using ProjectToTest.pages;

namespace Test.ExamplePageTest
{
    [TestFixture]
    public class LoginPageTest : BaseTest
    {
        TestContext testContext;
        LoginPage LoginPage;
        IWebDriver driver;

        [SetUp]        
        public void Initialize()
        {            
            testContext = TestContext.CurrentContext;
            reportHelper.StartTest(testContext);            
        }

        [Test]
        [TestCaseSource(typeof(BaseTest), "BrowserToRunWith")]
        public void TestReportLoginPage(string browserName)
        {
            driver = Setup(browserName);
            LoginPage = new LoginPage(driver);

            LoginPage.GoTo();
            LoginPage.BuscarGoogle("Hola Mundo");            
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
