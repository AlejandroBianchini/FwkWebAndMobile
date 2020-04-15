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
            Pages.HomePage.GoTo();
            testContext = TestContext.CurrentContext;
            reportHelper.Start(testContext);
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
            reportHelper.GenerateReport(testContext);
            Pages.HomePage.Quit();
        }
    }
}
