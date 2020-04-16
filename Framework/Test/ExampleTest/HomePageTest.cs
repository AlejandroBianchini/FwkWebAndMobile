using Core.Helpers;
using NUnit.Framework;
using ProjectToTest;

namespace TestProject.ExamplePageTest
{
    [TestFixture]
    [Parallelizable]
    public class HomePageTest
    {
        public TestContext testContext; //{ get; set; }
        private ReportHelper reportHelper = new ReportHelper();

        [SetUp]        
        public void Initialize()
        {
            Pages.HomePage.Initializes();
            testContext = TestContext.CurrentContext;
            
        }

        [Test]        
        public void TestReport()
        {
            Pages.HomePage.BuscarGoogle("Selenium");

            Assert.IsTrue(true);
        }

        [Test]
        public void TestReport2()
        {
           
            Assert.IsTrue(true);
        }

        [TearDown]
        public void CleanUp()
        {
            reportHelper.Start(testContext);
            reportHelper.GenerateReport(testContext);
            Pages.HomePage.Quit();
        }
    }
}
