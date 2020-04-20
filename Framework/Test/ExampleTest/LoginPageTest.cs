using Core.Helpers;
using NUnit.Framework;
using ProjectToTest;

namespace TestProject.ExamplePageTest
{
    [TestFixture]
    public class LoginPageTest
    {
        private TestContext testContext;
        private ReportHelper reportHelper = new ReportHelper();

        [SetUp]        
        public void Initialize()
        {
            Pages.LoginPage.Initializes();
            testContext = TestContext.CurrentContext;
            
        }

        [Test]        
        public void TestReportLoginPage()
        {            
            Pages.LoginPage.BuscarGoogle("Selenium");
            Pages.HomePage.BuscarGoogle("nada");
            Assert.IsTrue(true);
        }

        [TearDown]
        public void CleanUp()
        {
            reportHelper.GenerateReport(testContext);
            Pages.LoginPage.Quit();
        }
    }
}
